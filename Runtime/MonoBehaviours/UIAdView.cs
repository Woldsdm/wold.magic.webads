using MagicWebAds.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MagicWebAds
{
    [AddComponentMenu("Magic WebAds/UI Ad View")]
    public class UIAdView : Image
    {
        [SerializeField] bool launchOnEnable = true;
        [SerializeField] bool loadOnEnable = true;
        [SerializeField] bool showOnLoad = true;
        [SerializeField] bool hideWhenDisabled = true;

        [SerializeField]
        WebAdsListener listener = new();
        WebAds ads;


        protected override void Reset()
        {
            base.Reset();

            color = Color.magenta;
            sprite = null;
            type = Type.Simple;
            material = null;
            raycastTarget = false;
            preserveAspect = false;
            fillCenter = true;
            fillMethod = FillMethod.Horizontal;
            fillAmount = 1f;
            fillClockwise = true;
            fillOrigin = 0;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);

            if (Application.isPlaying)
            {
                vh.Clear();
                return;
            }

            Rect rect = GetPixelAdjustedRect();
            float thickness = 8;

            Vector2 bl = new(rect.xMin, rect.yMin);
            Vector2 tl = new(rect.xMin, rect.yMax);
            Vector2 tr = new(rect.xMax, rect.yMax);
            Vector2 br = new(rect.xMax, rect.yMin);

            Color lineColor = Color.black;

            void AddQuad(Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3)
            {
                int startIndex = vh.currentVertCount;

                vh.AddVert(v0, lineColor, Vector2.zero);
                vh.AddVert(v1, lineColor, Vector2.zero);
                vh.AddVert(v2, lineColor, Vector2.zero);
                vh.AddVert(v3, lineColor, Vector2.zero);

                vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
                vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
            }

            AddQuad(
                new(bl.x, bl.y),
                new(bl.x, bl.y + thickness),
                new(br.x, br.y + thickness),
                new(br.x, br.y)
            );

            AddQuad(
                new(tl.x, tl.y - thickness),
                new(tl.x, tl.y),
                new(tr.x, tr.y),
                new(tr.x, tr.y - thickness)
            );

            AddQuad(
                new(bl.x, bl.y),
                new(bl.x + thickness, bl.y),
                new(tl.x + thickness, tl.y),
                new(tl.x, tl.y)
            );

            AddQuad(
                new(br.x - thickness, br.y),
                new(br.x, br.y),
                new(tr.x, tr.y),
                new(tr.x - thickness, tr.y)
            );
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!Application.isPlaying) return;

            if (launchOnEnable && ads == null) Launch();

            if (loadOnEnable) Load();

            Debug.Log("OnEnable");
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (!Application.isPlaying) return;

            if (hideWhenDisabled) ads?.driver?.Close();

            Debug.Log("OnDisable");
        }

        public void Load()
        {

        }

        public void Launch()
        {
            listener.OnLoaded.AddListener(OnLoaded);
            ads = new(listener);
        }

        void OnLoaded()
        {
            if (showOnLoad) ads.driver.Show();
        }

        public void Dispose()
        {
            listener.Dispose();
            ads.Dispose();
            ads = null;
        }
    }
}

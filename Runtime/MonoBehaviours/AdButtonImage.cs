using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MagicWebAds
{
    [AddComponentMenu("Magic WebAds/Ad Button Image")]
    public class AdButtonImage : Image
    {
        [SerializeField]
        UnityEvent onClicked = new();
        public UnityEvent OnClicked => onClicked;
        public RectTransform rect { private set; get; }
        protected override void Reset()
        {
            base.Reset();

            color = Color.white;
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

            if (Application.isPlaying) vh.Clear();
        }

        protected override void Awake()
        {
            base.Awake();
            rect = GetComponent<RectTransform>();
        }
    }
}
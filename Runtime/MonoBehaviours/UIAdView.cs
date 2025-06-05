using MagicWebAds.Core;
using MagicWebAds.Core.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicWebAds
{
    [AddComponentMenu("Magic WebAds/UI Ad View")]
    public class UIAdView : Image
    {
        [Tooltip("Automatically initializes and launches the ad system on Enable.")]
        [SerializeField]
        bool launchOnEnable = true;

        [Tooltip("Automatically loads ads when the component is enabled.")]
        [SerializeField]
        bool loadOnEnable = true;

        [Tooltip("Displays the ad immediately after it is loaded.")]
        [SerializeField]
        bool showOnLoad = true;

        [Tooltip("Closes the ad when the component is disabled.")]
        [SerializeField]
        bool hideWhenDisabled = true;

        [Tooltip("List of keywords to filter and display relevant ads only.")]
        [SerializeField]
        List<string> filters = new();

        [Tooltip("List of custom buttons that appear inside the WebView ad content. Each button has an image and a callback.")]
        [SerializeField]
        List<AdButtonImage> adButtons = new();

        [Tooltip("Listener for receiving WebAd events (e.g., OnLoaded, OnClicked). Usually not needed.")]
        [SerializeField]
        WebAdsListener listener = new();

        WebAds ads;
        WebAdSettings adSettings;
        List<WebAdRequest> adRequests;

#if UNITY_EDITOR
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
#endif
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

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (Application.isPlaying) return;

            if (!launchOnEnable && loadOnEnable) launchOnEnable = true;
        }
#endif
        protected override void OnEnable()
        {
            base.OnEnable();
            if (!Application.isPlaying) return;

            if (launchOnEnable && ads == null) Launch();

            if (loadOnEnable) Load();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (!Application.isPlaying) return;

            if (hideWhenDisabled) ads?.driver?.Close();
        }

        public void Load()
        {
            if (ads != null && adRequests != null && adRequests.Count > 0)
            {
                var adRequest = adRequests[Random.Range(0, adRequests.Count)];
                if (adRequest.settings && adSettings != adRequest.settings)
                {
                    adSettings = adRequest.settings;
                    ads.settings = adSettings;
                    ads.ApplySettings();
                }
                ads.driver.SetAdLayout(rectTransform);
                ads.driver?.Load(adRequest);
            }
            else
            {
                Debug.LogError("Cannot load ad: Make sure 'Launch()' was called and successfully initialized the system before calling 'Load()'.");
            }
        }

        public void Show()
        {
            ads.driver.Show();
            if (adButtons.Count > 0)
            {
                foreach (var adButton in adButtons)
                {
                    adButton.Show();
                }
            }
        }

        public void Close()
        {
            ads.driver.Close();
        }

        public void Launch()
        {
            if (WebAdsManager.Instance)
            {
                adRequests = WebAdsManager.Instance.GetAdRequests(filters);

                if (adRequests.Count > 0)
                {
                    listener.OnLoaded.AddListener(OnLoaded);
                    listener.OnButtonClicked.AddListener(OnButtonClicked);
                    ads = new(listener);

                    if (adButtons.Count > 0)
                    {
                        foreach (var adButton in adButtons)
                        {
                            adButton.Launch(this);
                        }
                    }
                }
                else Debug.LogError("No WebAdRequests found for the given filters. Please check your filters or configure ad requests in WebAdsManager.");
            }
            else Debug.LogError("WebAdsManager is missing in the scene. Please add a WebAdsManager component before calling ad requests.");
        }

        public int AddButton(RectTransform rectTransform, Sprite sprite) => ads.driver.AddButton(rectTransform, sprite);

        public void SetButtonActive(int index, bool active) => ads.driver.SetButtonActive(index, active);

        public void UpdateButton(int index, Sprite sprite) => ads.driver.UpdateButton(index, sprite);

        void OnLoaded()
        {
            if (showOnLoad) Show();
        }

        void OnButtonClicked(int index)
        {
            adButtons[index].OnClicked.Invoke();
        }

        public void Dispose()
        {
            listener.Dispose();
            ads.Dispose();
            ads = null;
        }
    }
}

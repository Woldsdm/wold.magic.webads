using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using MagicWebAds;

public static class UIAdViewMenu
{
    [MenuItem("GameObject/UI/Magic WebAds/UI Ad View", false, 10)]
    public static void CreateUIAdView(MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;
        if (parent == null || parent.GetComponentInParent<Canvas>() == null)
        {
            GameObject canvas = new GameObject("Canvas", typeof(Canvas));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            Undo.RegisterCreatedObjectUndo(canvas, "Create Canvas");
            parent = canvas;
        }

        GameObject go = new GameObject("UI Ad View", typeof(RectTransform), typeof(UIAdView));
        GameObjectUtility.SetParentAndAlign(go, parent);

        RectTransform rt = go.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(1080, 141);

        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        UIAdView adView = go.GetComponent<UIAdView>();
        adView.color = new Color(0.6f, 0.2f, 0.8f, 1f);

        Selection.activeGameObject = go;
        Undo.RegisterCreatedObjectUndo(go, "Create UI Ad View");
    }
}
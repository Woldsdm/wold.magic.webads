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

        go.GetComponent<RectTransform>();

        go.GetComponent<UIAdView>();

        Selection.activeGameObject = go;
        Undo.RegisterCreatedObjectUndo(go, "Create UI Ad View");
    }
}
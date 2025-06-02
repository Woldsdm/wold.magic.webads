using MagicWebAds;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class AdButtonImageMenu
{
    [MenuItem("GameObject/UI/Magic WebAds/Ad Button Image", false, 11)]
    public static void CreateAdButton(MenuCommand menuCommand)
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

        GameObject go = new GameObject("Ad Button Image", typeof(RectTransform), typeof(AdButtonImage));
        GameObjectUtility.SetParentAndAlign(go, parent);

        Selection.activeGameObject = go;
        Undo.RegisterCreatedObjectUndo(go, "Create Ad Button Image");
    }
}

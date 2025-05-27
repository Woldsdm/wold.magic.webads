using System;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public static class AndroidHelper
    {
        public static void ConvertRectTransform(RectTransform rectTransform, out int x, out int y, out int width, out int height)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            Vector3 bottomLeft = worldCorners[0];
            Vector3 topRight = worldCorners[2];

            Vector2 bottomLeftScreenPoint = RectTransformUtility.WorldToScreenPoint(null, bottomLeft);
            Vector2 topRightScreenPoint = RectTransformUtility.WorldToScreenPoint(null, topRight);

            x = Mathf.RoundToInt(bottomLeftScreenPoint.x);
            y = Mathf.RoundToInt(Screen.height - topRightScreenPoint.y);
            width = Mathf.RoundToInt(topRightScreenPoint.x - bottomLeftScreenPoint.x);
            height = Mathf.RoundToInt(topRightScreenPoint.y - bottomLeftScreenPoint.y);
        }
        public static string SpriteToBase64(Sprite sprite)
        {
            Rect rect = sprite.rect;
            int width = (int)rect.width;
            int height = (int)rect.height;

            Texture2D newTex = new Texture2D(width, height, TextureFormat.RGBA32, false);

            Color[] pixels = sprite.texture.GetPixels(
                (int)rect.x, (int)rect.y, width, height
            );

            newTex.SetPixels(pixels);
            newTex.Apply();

            byte[] pngData = newTex.EncodeToPNG();
            UnityEngine.Object.Destroy(newTex);

            return Convert.ToBase64String(pngData);
        }
    }
}
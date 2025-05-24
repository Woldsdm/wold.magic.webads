using UnityEngine;

namespace MagicWebAds.Core.Android
{
    internal static class AndroidHelper
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
            if (sprite == null || sprite.texture == null)
                return "";

            var texture = sprite.texture;
            var rect = sprite.rect;

            Texture2D cropped = new Texture2D((int)rect.width, (int)rect.height, texture.format, false);
            cropped.SetPixels(texture.GetPixels(
                (int)rect.x,
                (int)rect.y,
                (int)rect.width,
                (int)rect.height
            ));
            cropped.Apply();

            byte[] imageData = cropped.EncodeToPNG();
            Object.Destroy(cropped);

            return System.Convert.ToBase64String(imageData);
        }
    }
}
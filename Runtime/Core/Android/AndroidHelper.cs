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
        public static byte[] SpriteToBytes(Sprite sprite, int maxWidth = -1, int maxHeight = -1)
        {
            Rect rect = sprite.rect;
            int width = (int)rect.width;
            int height = (int)rect.height;

            Texture2D extractedTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            Color[] pixels = sprite.texture.GetPixels((int)rect.x, (int)rect.y, width, height);
            extractedTex.SetPixels(pixels);
            extractedTex.Apply();

            Texture2D finalTex = extractedTex;

            if (maxWidth > 0 && maxHeight > 0 && (width > maxWidth || height > maxHeight))
            {
                float scaleX = (float)maxWidth / width;
                float scaleY = (float)maxHeight / height;
                float scale = Mathf.Min(scaleX, scaleY);

                int newWidth = Mathf.RoundToInt(width * scale);
                int newHeight = Mathf.RoundToInt(height * scale);

                Texture2D resizedTex = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
                Color[] resizedPixels = TextureScaler.Bilinear(extractedTex.GetPixels(), width, height, newWidth, newHeight);
                resizedTex.SetPixels(resizedPixels);
                resizedTex.Apply();

                UnityEngine.Object.Destroy(extractedTex);

                Texture2D paddedTex = new Texture2D(maxWidth, maxHeight, TextureFormat.RGBA32, false);
                Color[] transparentPixels = new Color[maxWidth * maxHeight];
                for (int i = 0; i < transparentPixels.Length; i++)
                    transparentPixels[i] = new Color(0, 0, 0, 0);

                paddedTex.SetPixels(transparentPixels);

                int offsetX = (maxWidth - newWidth) / 2;
                int offsetY = (maxHeight - newHeight) / 2;

                for (int y = 0; y < newHeight; y++)
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        Color c = resizedTex.GetPixel(x, y);
                        paddedTex.SetPixel(x + offsetX, y + offsetY, c);
                    }
                }

                paddedTex.Apply();

                UnityEngine.Object.Destroy(resizedTex);
                finalTex = paddedTex;
            }

            byte[] pngData = finalTex.EncodeToPNG();
            UnityEngine.Object.Destroy(finalTex);
            return pngData;
        }


        public static class TextureScaler
        {
            public static Color[] Bilinear(Color[] original, int originalWidth, int originalHeight, int newWidth, int newHeight)
            {
                Color[] newPixels = new Color[newWidth * newHeight];
                float ratioX = (float)(originalWidth - 1) / newWidth;
                float ratioY = (float)(originalHeight - 1) / newHeight;

                for (int y = 0; y < newHeight; y++)
                {
                    float yy = y * ratioY;
                    int y1 = Mathf.FloorToInt(yy);
                    int y2 = Mathf.Min(y1 + 1, originalHeight - 1);
                    float fy = yy - y1;

                    for (int x = 0; x < newWidth; x++)
                    {
                        float xx = x * ratioX;
                        int x1 = Mathf.FloorToInt(xx);
                        int x2 = Mathf.Min(x1 + 1, originalWidth - 1);
                        float fx = xx - x1;

                        Color bl = original[y1 * originalWidth + x1];
                        Color br = original[y1 * originalWidth + x2];
                        Color tl = original[y2 * originalWidth + x1];
                        Color tr = original[y2 * originalWidth + x2];

                        Color b = Color.Lerp(bl, br, fx);
                        Color t = Color.Lerp(tl, tr, fx);
                        newPixels[y * newWidth + x] = Color.Lerp(b, t, fy);
                    }
                }
                return newPixels;
            }
        }
    }
}
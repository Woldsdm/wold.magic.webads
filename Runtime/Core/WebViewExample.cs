using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebViewExample : MonoBehaviour
{
    private AndroidJavaObject webView;

    public void Test()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    webView = new AndroidJavaObject("android.webkit.WebView", currentActivity);

                    AndroidJavaObject layoutParams = new AndroidJavaObject(
                        "android.widget.FrameLayout$LayoutParams",
                        -1, // MATCH_PARENT
                        -1  // MATCH_PARENT
                    );
                    webView.Call("setLayoutParams", layoutParams);

                    AndroidJavaObject settings = webView.Call<AndroidJavaObject>("getSettings");
                    settings.Call("setJavaScriptEnabled", true);

                    webView.Call("loadUrl", "https://github.com/");

                    AndroidJavaObject layout = currentActivity.Call<AndroidJavaObject>("findViewById", 0x01020002); // android.R.id.content
                    layout.Call("addView", webView);
                }));
            }
        }
        else
        {
            Debug.Log("Device is not Android");
        }
    }

    void OnDestroy()
    {
        if (webView != null)
        {
            webView.Call("destroy");
            webView = null;
        }
    }

    void Start()
    {
        StartCoroutine(TestRequest());
    }
    IEnumerator TestRequest()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://www.google.com"))
        {
            yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
            if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError("Request Failed: " + request.error);
            }
            else
            {
                Debug.Log("Request Success: " + request.downloadHandler.text.Substring(0, 100)); 
            }
        }
    }
}


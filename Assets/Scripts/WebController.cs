using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class WebController
{
    public static async Task<Texture2D> DownloadTexture(string url)
    {
        try
        {
            using var www = UnityWebRequestTexture.GetTexture(url);
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Request failed: {www.error}");
            }
            var result = ((DownloadHandlerTexture)www.downloadHandler).texture;
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Request failed: {ex.Message}");
            return default;
        }
    }

    public static void OpenUrl(string actionParameterText)
    {
        try
        {
            Application.OpenURL(actionParameterText);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Request failed: {ex.Message}");
        }
    }
}

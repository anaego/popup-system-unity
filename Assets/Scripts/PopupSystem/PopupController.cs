using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PopupController
{
    private PopupView view;

    private bool isShown;

    public bool IsShown 
    { 
        get => isShown; 
        set
        {
            isShown = value;
            view.IsShown = value;
        }
    }

    public PopupController(PopupView view)
    {
        this.view = view;
    }

    internal void SetLastVisually()
    {
        view.SetLastVisually();
    }

    internal async void SetupViewWithData(PopupData popupData)
    {
        view.TitleText = popupData.PopupTitle;
        view.ContentText = popupData.PopupTitle;
        view.ButtonText = popupData.ButtonText;
        // TODO refactor this to execute at the same time ish
        if (!String.IsNullOrEmpty(popupData.BackgroundImageUrl))
        {
            var task = DownloadTexture(popupData.BackgroundImageUrl);
            await task;
            view.BackgroundImage = task.Result;
        }
        if (!String.IsNullOrEmpty(popupData.ButtonImageUrl))
        {
            var task = DownloadTexture(popupData.ButtonImageUrl);
            await task;
            view.ButtonImage = task.Result;
        }
        view.ButtonAction = popupData.ButtonAction;
        IsShown = true;
        FadeAnimation.Animate(view.CanvasGroup, null);
    }

    // TODO To separate script
    public async Task<Texture2D> DownloadTexture(string url)
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
}

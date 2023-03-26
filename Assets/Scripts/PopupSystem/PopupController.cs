using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PopupController
{
    private PopupView view;
    private PopupSettingsScriptableObject settings;
    private FadeAnimation animation;

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

    public PopupController(PopupView view, PopupSettingsScriptableObject settings)
    {
        this.view = view;
        this.settings = settings;
    }

    internal void SetLastVisually()
    {
        view.SetLastVisually();
    }

    internal async void SetupViewWithData(PopupData popupData, Action<PopupController> onEnd)
    {
        if (!String.IsNullOrEmpty(popupData.PopupTitle))
        {
            view.TitleText = popupData.PopupTitle;
        }
        if (!String.IsNullOrEmpty(popupData.PopupContent))
        {
            view.ContentText = popupData.PopupContent;
        }
        if (!String.IsNullOrEmpty(popupData.ButtonText))
        {
            view.ButtonText = popupData.ButtonText;
        }
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
        if (popupData.ButtonAction != null)
        {
            view.ButtonAction = popupData.ButtonAction;
        }
        IsShown = true;
        animation = new FadeAnimation();
        if (popupData.ButtonAction != null)
        {
            animation.Animate(
                view.CanvasGroup,
                settings.FadeInDuration);
            view.ButtonAction = () => FadeOutAndEnd(onEnd);
            return;
        }
        animation.Animate(
            view.CanvasGroup,
            settings.FadeInDuration,
            settings.FadeWaitDuration,
            settings.FadeOutDuration,
            () => EndPopup(onEnd));
    }

    private void FadeOutAndEnd(Action<PopupController> onEnd)
    {
        animation.Animate(
            view.CanvasGroup, 
            settings.FadeWaitDuration, 
            settings.FadeOutDuration,
            () => EndPopup(onEnd));
    }

    private void EndPopup(Action<PopupController> onEnd)
    {
        IsShown = false;
        onEnd.Invoke(this);
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

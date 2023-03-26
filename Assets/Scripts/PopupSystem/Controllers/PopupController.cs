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
    private EffectPlayerView effectPlayer;

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

    public PopupController(
        PopupView view, PopupSettingsScriptableObject settings, EffectPlayerView effectPlayer)
    {
        this.view = view;
        this.settings = settings;
        this.effectPlayer = effectPlayer;
    }

    internal void SetLastVisually()
    {
        view.SetLastVisually();
    }

    internal async void SetupViewWithData(PopupData popupData, Action<PopupController> onEnd)
    {
        view.IsContentVisible = popupData.IsContentVisible;
        view.IsButtonVisible = popupData.IsButtonVisible;
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
        else
        {
            view.BackgroundImage = settings.DefaultBackgroundImage;
        }
        if (!String.IsNullOrEmpty(popupData.ButtonImageUrl))
        {
            var task = DownloadTexture(popupData.ButtonImageUrl);
            await task;
            view.ButtonImage = task.Result;
        }
        else
        {
            view.ButtonImage = settings.DefaultButtonImage;
        }
        if (popupData.ButtonActionData != null)
        {
            SetupButtonAction(popupData.ButtonActionData, onEnd);
        }
        IsShown = true;
        //TODO Extract method
        animation = new FadeAnimation();
        if (popupData.ButtonActionData != null)
        {
            animation.Animate(view.CanvasGroup, settings.FadeInDuration);
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

    private void SetupButtonAction(
        ButtonActionData buttonActionData, Action<PopupController> onEnd)
    {
        switch (buttonActionData.ButtonActionType)
        {
            case PopupActionType.None:
                view.ButtonAction = EmptyAction;
                break;
            case PopupActionType.OpenUrl:
                view.ButtonAction = () => OpenUrl(buttonActionData.ActionParameterText);
                break;
            case PopupActionType.ClosePopup:
                view.ButtonAction = () => EndImmediately(onEnd);
                break;
            case PopupActionType.PlayAnimaOrEffect:
                view.ButtonAction = () => SpawnEffect(buttonActionData.ActionParameterEffectType);
                break;
            case PopupActionType.CustomFromEditor:
                view.ButtonAction = buttonActionData.CustomTestPanelAction;
                break;
            case PopupActionType.Random:
                buttonActionData.ResetType(
                    (PopupActionType)(new System.Random().Next(
                        2,
                        Enum.GetNames(typeof(PopupActionType)).Length + 1)));
                SetupButtonAction(buttonActionData, onEnd);
                break;
            default:
                Debug.Log($"Unknown Popup Action Type: {buttonActionData.ButtonActionType}");
                break;
        }
    }

    // TODO to different class?
    private void SpawnEffect(ActionEffectType actionEffectType)
    {
        switch (actionEffectType)
        {
            case ActionEffectType.None:
                break;
            case ActionEffectType.FlashAnimation:
            case ActionEffectType.ParticleSystem:
                effectPlayer.PlayEffect(actionEffectType);
                break;
            case ActionEffectType.Random:
                SpawnEffect((ActionEffectType)(new System.Random().Next(
                    2,
                    Enum.GetNames(typeof(ActionEffectType)).Length + 1)));
                break;
            default:
                Debug.Log($"Unknown Action Effect Type: {actionEffectType}");
                break;
        }
    }

    // TODO to different class
    private void OpenUrl(string actionParameterText)
    {
        try
        {
            Application.OpenURL(actionParameterText);
        }
        catch(Exception ex)
        {
            Debug.LogError($"Request failed: {ex.Message}");
        }
    }

    private void EmptyAction()
    {
    }

    private void EndImmediately(Action<PopupController> onEnd)
    {
        animation.StopAnimating();
        onEnd.Invoke(this);
    }

    private void FadeOutAndEnd(Action<PopupController> onEnd)
    {
        animation.Animate(
            view.CanvasGroup, 
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

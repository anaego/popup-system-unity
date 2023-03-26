using System;
using System.Threading.Tasks;
using UnityEngine;

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
        Task<Texture2D> bgImageTask = null;
        Task<Texture2D> buttonImageTask = null;
        if (!String.IsNullOrEmpty(popupData.BackgroundImageUrl))
        {
            bgImageTask = WebController.DownloadTexture(popupData.BackgroundImageUrl);
        }
        else
        {
            view.BackgroundImage = settings.DefaultBackgroundImage;
        }
        if (!String.IsNullOrEmpty(popupData.ButtonImageUrl))
        {
            buttonImageTask = WebController.DownloadTexture(popupData.ButtonImageUrl);
        }
        else
        {
            view.ButtonImage = settings.DefaultButtonImage;
        }
        if (popupData.ButtonActionData != null)
        {
            SetupButtonAction(popupData.ButtonActionData, onEnd);
        }
        if (bgImageTask != null)
        {
            await bgImageTask;
            view.BackgroundImage = bgImageTask.Result;
        }
        if (buttonImageTask != null)
        {
            await buttonImageTask;
            view.ButtonImage = buttonImageTask.Result;
        }
        IsShown = true;
        SetupAnimation(popupData, onEnd);
    }

    private void SetupAnimation(PopupData popupData, Action<PopupController> onEnd)
    {
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
                view.ButtonAction = () => WebController.OpenUrl(buttonActionData.ActionParameterText);
                break;
            case PopupActionType.ClosePopup:
                view.ButtonAction = () => EndImmediately(onEnd);
                break;
            case PopupActionType.PlayAnimOrEffect:
                view.ButtonAction = () => PlayEffect(buttonActionData.ActionParameterEffectType);
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

    private void PlayEffect(ActionEffectType actionEffectType)
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
                PlayEffect((ActionEffectType)(new System.Random().Next(
                    2,
                    Enum.GetNames(typeof(ActionEffectType)).Length + 1)));
                break;
            default:
                Debug.Log($"Unknown Action Effect Type: {actionEffectType}");
                break;
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
}

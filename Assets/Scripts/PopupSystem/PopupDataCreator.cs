using System;
using System.Collections;
using System.Collections.Generic;

public class PopupDataCreator
{
    public static PopupData CreatePopupData(
        PopupType popupType,
        string title = null,
        string content = null,
        string buttonText = null,
        string backgroundImageUrl = null,
        string buttonImageUrl = null,
        Action buttonAction = null)
    {
        var popupData = new PopupData()
        {
            PopupTitle = title,
            PopupContent = content,
            ButtonText = buttonText,
            BackgroundImageUrl = backgroundImageUrl,
            ButtonImageUrl = buttonImageUrl,
            ButtonAction = buttonAction
        };
        SetupPopupDataVisibility(popupType, popupData);
        return popupData;
    }

    private static void SetupPopupDataVisibility(PopupType popupType, PopupData popupData)
    {
        switch (popupType)
        {
            
            case PopupType.TitleNoContentNoButton:
                SetDataVisibility(popupData, false, false);
                break;
            case PopupType.TitleNoContentButton:
                SetDataVisibility(popupData, false, true);
                break;
            case PopupType.TitleContentButton:
                SetDataVisibility(popupData, true, true);
                break;
            case PopupType.Random:
            default:
                SetupPopupDataVisibility(
                    (PopupType)(new Random().Next(
                        1,
                        Enum.GetNames(typeof(PopupType)).Length)),
                    popupData);
                break;
        }
    }

    private static void SetDataVisibility(PopupData popupData, bool isContentVisible, bool isButtonVisible)
    {
        popupData.IsContentVisible = isContentVisible;
        popupData.IsButtonVisible = isButtonVisible;
    }
}

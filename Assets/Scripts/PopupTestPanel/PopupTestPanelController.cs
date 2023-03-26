using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static TMPro.TMP_Dropdown;
using UnityEngine.Events;

public class PopupTestPanelController 
{
    private PopupTestPanelView view;
    private PopupSettingsScriptableObject popupSettingsSO;
    private Transform popupParent;
    private EffectPlayerView effectPlayer;

    public PopupTestPanelController(
        PopupTestPanelView popupTestPanelView, 
        PopupSettingsScriptableObject settings, 
        Transform popupParent, 
        EffectPlayerView effectPlayer)
    {
        view = popupTestPanelView;
        popupSettingsSO = settings; 
        this.popupParent = popupParent;
        this.effectPlayer = effectPlayer;
        SetupPopupTypeDropdown();
        SetupPopupActionTypeDropdown();
        SetupPopupActionEffectTypeDropdown();
        SetupPopupNumberDropdown();
        SetupSpawnButtonAction();
    }

    private void SetupPopupTypeDropdown()
    {
        var popupTypeList = Enum.GetValues(typeof(PopupType)).Cast<PopupType>()
            .Select(popupType => popupType.ToString())
            .ToList();
        view.PopupTypeDropdownOptions = popupTypeList;
        view.OnPopupTypeDropdownValueChanged = UpdateElementsVisibility;
    }

    private void UpdateElementsVisibility(int index)
    {
        switch ((PopupType)index)
        {
            case PopupType.TitleNoContentNoButton:
                SetElementsVisibility(true, false, false, false, false);
                break;
            case PopupType.TitleNoContentButton:
                SetElementsVisibility(true, false, true, true, true);
                break;
            case PopupType.TitleContentButton:
            case PopupType.Random:
                SetElementsVisibility(true, true, true, true, true);
                break;
            default:
                SetElementsVisibility(true, true, true, true, true);
                Debug.Log($"Unknown popup type {(PopupType)index}");
                break;
        }
    }

    private void SetElementsVisibility(
        bool isPopupTitleTextInputActive, 
        bool isPopupContentTextInputActive, 
        bool isPopupButtonTextInputActive, 
        bool isPopupButtonImageUrlInputActive,
        bool isPopupButtonActionInputActive)
    {
        view.IsPopupTitleTextInputActive = isPopupTitleTextInputActive;
        view.IsPopupContentTextInputActive = isPopupContentTextInputActive;
        view.IsPopupButtonTextInputActive = isPopupButtonTextInputActive;
        view.IsPopupButtonImageUrlInputActive = isPopupButtonImageUrlInputActive;
        view.IsPopupButtonActionInputActive = isPopupButtonActionInputActive;
    }

    private void SetupPopupNumberDropdown()
    {
        var numberRange = Enumerable.Range(1, 10)
            .Select(number => number.ToString())
            .ToList();
        view.PopupsToSpawnNumberDropdownOptions = numberRange;
    }

    private void SetupPopupActionTypeDropdown()
    {
        var actionTypeList = Enum.GetValues(typeof(PopupActionType)).Cast<PopupActionType>()
            .Select(actionType => actionType.ToString())
            .ToList();
        view.PopupActionDropdownOptions = actionTypeList;
        view.OnPopupActionDropdownValueChanged = UpdateActionElementsVisibility;
    }

    private void UpdateActionElementsVisibility(int index)
    {
        switch ((PopupActionType)index)
        {
            case PopupActionType.OpenUrl:
                SetActionElementsVisibility(true, false);
                break;
            case PopupActionType.ClosePopup:
                SetActionElementsVisibility(false, false);
                break;
            case PopupActionType.PlayAnimaOrEffect:
                SetActionElementsVisibility(false, true);
                break;
            case PopupActionType.Random:
                SetActionElementsVisibility(true, true);
                break;
            default:
                SetActionElementsVisibility(false, false);
                Debug.Log($"Unknown popup action type {(PopupActionType)index}");
                break;
        }
    }

    private void SetActionElementsVisibility(bool isTextParameterInputActive, bool isEffectParameterInputActive)
    {
        view.IsTextParameterInputActive = isTextParameterInputActive;
        view.IsEffectParameterInputActive = isEffectParameterInputActive;
    }

    private void SetupPopupActionEffectTypeDropdown()
    {
        var effectTypeList = Enum.GetValues(typeof(ActionEffectType)).Cast<ActionEffectType>()
            .Select(actionType => actionType.ToString())
            .ToList();
        view.PopupActionEffectParameterDropdownOptions = effectTypeList;
    }

    private void SetupSpawnButtonAction()
    {
        view.SpawnPopupButtonAction = SpawnPopups;
    }

    private void SpawnPopups()
    {
        for (int i = 0; i < view.ChosenDropdownNumber; i++)
        {
            var buttonActionData = view.PopupButtonAction == PopupActionType.None 
                ? null 
                : new ButtonActionData(
                    view.PopupButtonAction, 
                    view.PopupButtonActionTextParameter,
                    view.PopupButtonActionEffectParameter,
                    view.CustomTestPanelAction);
            var popupData = PopupDataCreator.CreatePopupData(
                view.ChosenPopupType,
                view.TitleText,
                view.ContentText,
                view.ButtonText,
                view.BackgroundImadeUrl,
                view.ButtonImageUrl,
                buttonActionData);
            PopupQueue.GetInstance(popupSettingsSO, popupParent, effectPlayer).AddToQueue(popupData);
        }
    }
}

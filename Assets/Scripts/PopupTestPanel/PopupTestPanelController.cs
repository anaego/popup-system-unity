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

    public PopupTestPanelController(
        PopupTestPanelView popupTestPanelView, PopupSettingsScriptableObject settings, Transform popupParent)
    {
        view = popupTestPanelView;
        popupSettingsSO = settings; 
        this.popupParent = popupParent;
        SetupPopupTypeDropdown();
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
                SetElementsVisibility(true, false, false, false);
                break;
            case PopupType.TitleNoContentButton:
                SetElementsVisibility(true, false, true, true);
                break;
            case PopupType.TitleContentButton:
            case PopupType.Random:
                SetElementsVisibility(true, true, true, true);
                break;
            default:
                SetElementsVisibility(true, true, true, true);
                Debug.Log($"Unknown popup type {(PopupType)index}");
                break;
        }
    }

    private void SetElementsVisibility(
        bool isPopupTitleTextInputActive, 
        bool isPopupContentTextInputActive, 
        bool isPopupButtonTextInputActive, 
        bool isPopupButtonImageUrlInputActive)
    {
        view.IsPopupTitleTextInputActive = isPopupTitleTextInputActive;
        view.IsPopupContentTextInputActive = isPopupContentTextInputActive;
        view.IsPopupButtonTextInputActive = isPopupButtonTextInputActive;
        view.IsPopupButtonImageUrlInputActive = isPopupButtonImageUrlInputActive;
    }

    private void SetupPopupNumberDropdown()
    {
        var numberRange = Enumerable.Range(1, 10)
            .Select(number => number.ToString())
            .ToList();
        view.PopupsToSpawnNumberDropdownOptions = numberRange;
    }

    private void SetupSpawnButtonAction()
    {
        view.SpawnPopupButtonAction = SpawnPopups;
    }

    private void SpawnPopups()
    {
        for (int i = 0; i < view.ChosenDropdownNumber; i++)
        {
            var popupData = PopupDataCreator.CreatePopupData(
                view.ChosenPopupType,
                view.TitleText,
                view.ContentText,
                view.ButtonText,
                view.BackgroundImadeUrl,
                view.ButtonImageUrl,
                view.PopupButtonAction);
            PopupQueue.GetInstance(popupSettingsSO, popupParent).AddToQueue(popupData);
        }
    }
}

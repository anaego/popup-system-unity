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

    public PopupTestPanelController(PopupTestPanelView popupTestPanelView)
    {
        view = popupTestPanelView;
        SetupPopupTypeDropdown();
        SetupPopupNumberDropdown();
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
}

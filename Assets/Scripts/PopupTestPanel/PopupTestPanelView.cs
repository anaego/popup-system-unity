using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

// TODO mb remove "popup" from the var names
public class PopupTestPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown popupTypeDropdown;
    [SerializeField] private TMP_InputField popupBackgroundImageUrlInput;
    [SerializeField] private TMP_InputField popupButtonImageUrlInput;
    [SerializeField] private TMP_InputField popupTitleTextInput;
    [SerializeField] private TMP_InputField popupContentTextInput;
    [SerializeField] private TMP_InputField popupButtonTextInput;
    [SerializeField] private TMP_Dropdown popupsNumberDropdown;
    [SerializeField] private Button spawnPopupButton;
    [Header("Element Parents")]
    [SerializeField] private GameObject popupTitleTextInputContainer;
    [SerializeField] private GameObject popupContentTextInputContainer;
    [SerializeField] private GameObject popupButtonTextInputContainer;
    [SerializeField] private GameObject popupButtonImageUrlInputContainer;

    public Button SpawnPopupButton => spawnPopupButton;

    public List<string> PopupTypeDropdownOptions
    {
        set
        {
            popupTypeDropdown.AddOptions(
                value.Select(popupType => new OptionData(popupType.ToString())).ToList());
        }
    }
    public List<string> PopupsToSpawnNumberDropdownOptions
    {
        set
        {
            popupsNumberDropdown.AddOptions(
                value.Select(popupType => new OptionData(popupType.ToString())).ToList());
        }
    }
    public Action<int> OnPopupTypeDropdownValueChanged
    {
        set
        {
            popupTypeDropdown.onValueChanged.AddListener(index => value(index));
        }
    }
    public bool IsPopupTitleTextInputActive
    { 
        set 
        {
            popupTitleTextInputContainer.gameObject.SetActive(value);
        } 
    }
    public bool IsPopupContentTextInputActive
    {
        set
        {
            popupContentTextInputContainer.gameObject.SetActive(value);
        }
    }
    public bool IsPopupButtonTextInputActive
    {
        set
        {
            popupButtonTextInputContainer.gameObject.SetActive(value);
        }
    }
    public bool IsPopupButtonImageUrlInputActive
    {
        set
        {
            popupButtonImageUrlInputContainer.gameObject.SetActive(value);
        }
    }
}

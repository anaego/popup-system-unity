using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

public class PopupTestPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown popupTypeDropdown;
    [SerializeField] private TMP_InputField popupBackgroundImageUrlInput;
    [SerializeField] private TMP_InputField popupButtonImageUrlInput;
    [SerializeField] private TMP_InputField popupTitleTextInput;
    [SerializeField] private TMP_InputField popupContentTextInput;
    [SerializeField] private TMP_InputField popupButtonTextInput;
    [SerializeField] private TMP_Dropdown popupButtonActionDropdown;
    [SerializeField] private TMP_Dropdown popupsNumberDropdown;
    [SerializeField] private Button spawnPopupButton;
    [Header("Button Action Parameters")]
    [SerializeField] private TMP_InputField popupActionTextInput;
    [SerializeField] private TMP_Dropdown popupActionEffectDropdown;
    [SerializeField] private UnityEvent customTestPanelAction;
    [Header("Element Parents")]
    [SerializeField] private GameObject popupTitleTextInputContainer;
    [SerializeField] private GameObject popupContentTextInputContainer;
    [SerializeField] private GameObject popupButtonTextInputContainer;
    [SerializeField] private GameObject popupButtonImageUrlInputContainer;
    [SerializeField] private GameObject popupButtonActionInputContainer;
    [SerializeField] private GameObject actionTextParameterInputContainer;
    [SerializeField] private GameObject actionEffectParameterInputContainer;


    public PopupType ChosenPopupType => (PopupType)popupTypeDropdown.value;
    public string TitleText => popupTitleTextInput.text;
    public string ContentText => popupContentTextInput.text;
    public string ButtonText => popupButtonTextInput.text;
    public string BackgroundImadeUrl => popupBackgroundImageUrlInput.text;
    public string ButtonImageUrl => popupButtonImageUrlInput.text;
    public PopupActionType PopupButtonAction => (PopupActionType)popupButtonActionDropdown.value;
    public string PopupButtonActionTextParameter => popupActionTextInput.text;
    public ActionEffectType PopupButtonActionEffectParameter => (ActionEffectType)popupActionEffectDropdown.value;
    public int ChosenDropdownNumber => int.Parse(popupsNumberDropdown.options[popupsNumberDropdown.value].text);
    public Action CustomTestPanelAction => () => customTestPanelAction.Invoke();

    public List<string> PopupTypeDropdownOptions
    {
        set => popupTypeDropdown.AddOptions(
                value.Select(popupType => new OptionData(popupType.ToString())).ToList());
    }
    public List<string> PopupsToSpawnNumberDropdownOptions
    {
        set => popupsNumberDropdown.AddOptions(
                value.Select(popupType => new OptionData(popupType.ToString())).ToList());
    }
    public List<string> PopupActionDropdownOptions
    {
        set => popupButtonActionDropdown.AddOptions(
                value.Select(actionType => new OptionData(actionType.ToString())).ToList());
    }
    public List<string> PopupActionEffectParameterDropdownOptions
    {
        set => popupActionEffectDropdown.AddOptions(
                value.Select(effectType => new OptionData(effectType.ToString())).ToList());
    }
    public Action<int> OnPopupTypeDropdownValueChanged
    {
        set => popupTypeDropdown.onValueChanged.AddListener(index => value(index));
    }
    public Action<int> OnPopupActionDropdownValueChanged
    {
        set => popupButtonActionDropdown.onValueChanged.AddListener(index => value(index));
    }
    public Action SpawnPopupButtonAction
    {
        set => spawnPopupButton.onClick.AddListener(value.Invoke);
    }

    public bool IsPopupTitleTextInputActive
    {
        set => popupTitleTextInputContainer.gameObject.SetActive(value);
    }
    public bool IsPopupContentTextInputActive
    {
        set => popupContentTextInputContainer.gameObject.SetActive(value);
    }
    public bool IsPopupButtonTextInputActive
    {
        set => popupButtonTextInputContainer.gameObject.SetActive(value);
    }
    public bool IsPopupButtonImageUrlInputActive
    {
        set => popupButtonImageUrlInputContainer.gameObject.SetActive(value);
    }
    public bool IsPopupButtonActionInputActive
    {
        set => popupButtonActionInputContainer.gameObject.SetActive(value);
    }
    public bool IsTextParameterInputActive
    {
        set => actionTextParameterInputContainer.gameObject.SetActive(value);
    }
    public bool IsEffectParameterInputActive
    {
        set => actionEffectParameterInputContainer.gameObject.SetActive(value);
    }
}

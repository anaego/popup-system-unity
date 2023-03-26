using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private GameObject popupContainer;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private RawImage buttonImage;
    [SerializeField] private Button button;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject contentContainer;
    [SerializeField] private GameObject buttonContainer;

    public CanvasGroup CanvasGroup => canvasGroup;


    public bool IsShown
    {
        set => popupContainer.SetActive(value);
    }
    public string TitleText
    {
        set => titleText.text = value;
    }
    public string ContentText
    {
        set => contentText.text = value;
    }
    public string ButtonText
    {
        set => buttonText.text = value;
    }
    public Texture2D BackgroundImage
    {
        set => backgroundImage.texture = value;
    }
    public Texture2D ButtonImage
    {
        set => buttonImage.texture = value;
    }
    public Action ButtonAction
    {
        set => button.onClick.AddListener(value.Invoke);
    }
    public bool IsContentVisible
    {
        set => contentContainer.SetActive(value);
    }
    public bool IsButtonVisible
    {
        set => buttonContainer.SetActive(value);
    }

    internal void SetLastVisually()
    {
        popupContainer.transform.SetAsLastSibling();
    }
}

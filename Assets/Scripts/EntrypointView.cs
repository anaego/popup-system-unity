using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrypointView : MonoBehaviour
{
    [SerializeField] private Transform popupParent;
    [SerializeField] private PopupTestPanelView popupTestPanelView;
    [SerializeField] private PopupSettingsScriptableObject popupSettingsSO;

    private void Awake()
    {
        var popupTestPanelController = new PopupTestPanelController(
            popupTestPanelView, popupSettingsSO, popupParent);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrypointView : MonoBehaviour
{
    [SerializeField] private GameObject popupParent;
    [SerializeField] private PopupTestPanelView popupTestPanelView;

    private void Awake()
    {
        var popupTestPanelController = new PopupTestPanelController(popupTestPanelView);
    }
}

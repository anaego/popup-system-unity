using UnityEngine;

public class EntrypointView : MonoBehaviour
{
    [SerializeField] private Transform popupParent;
    [SerializeField] private PopupTestPanelView popupTestPanelView;
    [SerializeField] private PopupSettingsScriptableObject popupSettingsSO;
    [SerializeField] private EffectPlayerView effectPlayer;

    private void Awake()
    {
        var popupTestPanelController = new PopupTestPanelController(
            popupTestPanelView, popupSettingsSO, popupParent, effectPlayer);
    }
}

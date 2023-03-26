using System;

public class ButtonActionData
{
    public PopupActionType ButtonActionType { get; private set; }
    // TODO refactor to create a list of parameters or smth
    public string ActionParameterText { get; }
    public ActionEffectType ActionParameterEffectType { get; }
    public Action CustomTestPanelAction { get; }

    public ButtonActionData(
        PopupActionType buttonActionType = PopupActionType.None,
        string actionParameterText = null,
        ActionEffectType actionParameterEffectType = ActionEffectType.None,
        Action customTestPanelAction = null)
    {
        ButtonActionType = buttonActionType;
        ActionParameterText = actionParameterText;
        ActionParameterEffectType = actionParameterEffectType;
        CustomTestPanelAction = customTestPanelAction;
    }

    internal void ResetType(PopupActionType popupActionType)
    {
        ButtonActionType = popupActionType;
    }
}
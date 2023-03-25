using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupSettings", menuName = "ScriptableObjects/PopupSettingsScriptableObject", order = 1)]
public class PopupSettingsScriptableObject : ScriptableObject
{
    [SerializeField] private PopupView popupViewPrefab;
    [SerializeField] private int maxPopupsSimultaneously = 1;

    public PopupView PopupViewPrefab => popupViewPrefab;
    public int MaxPopupsSimultaneously => maxPopupsSimultaneously;

}

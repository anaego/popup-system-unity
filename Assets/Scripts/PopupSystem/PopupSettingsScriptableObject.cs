using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupSettings", menuName = "ScriptableObjects/PopupSettingsScriptableObject", order = 1)]
public class PopupSettingsScriptableObject : ScriptableObject
{
    [SerializeField] private PopupView popupViewPrefab;
    [SerializeField] private int maxPopupsSimultaneously = 1;
    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 2;
    [SerializeField] private float fadeWaitDuration = 5;
    [SerializeField] private float fadeOutDuration = 2;

    public PopupView PopupViewPrefab => popupViewPrefab;
    public int MaxPopupsSimultaneously => maxPopupsSimultaneously;
    public float FadeInDuration => fadeInDuration;
    public float FadeWaitDuration => fadeWaitDuration;
    public float FadeOutDuration => fadeOutDuration;
}

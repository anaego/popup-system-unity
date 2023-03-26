using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectPlayerView : MonoBehaviour
{
    [SerializeField] private List<PopupEffectMapping> effects;

    public void PlayEffect(ActionEffectType actionEffectType)
    {
        GetEffect(actionEffectType)?.Play();
    }

    internal PopupEffectView GetEffect(ActionEffectType actionEffectType)
    {
        return effects.FirstOrDefault(effect => effect.EffectType == actionEffectType).Effect;
    }
}

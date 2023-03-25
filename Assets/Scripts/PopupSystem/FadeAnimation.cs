using System;
using DG.Tweening;
using UnityEngine;

public class FadeAnimation
{
    public static void Animate(CanvasGroup canvasGroup, Action onEnd)
    {
        DOTween.Sequence()
            .Append(canvasGroup.DOFade(1, 3))
            .AppendInterval(3)
            .Append(canvasGroup.DOFade(0, 1));
    }
}

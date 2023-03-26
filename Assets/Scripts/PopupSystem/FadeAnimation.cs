using System;
using DG.Tweening;
using UnityEngine;

public class FadeAnimation
{
    private Sequence fadeSequence;

    public void Animate(
        CanvasGroup canvasGroup, 
        float fadeInDuration, 
        float waitDuration, 
        float fadeOutDuration, 
        Action onEnd)
    {
        fadeSequence = GetFadeInSequence(canvasGroup, fadeInDuration);
        fadeSequence = AppendFadeOutSequence(fadeSequence, canvasGroup, waitDuration, fadeOutDuration, onEnd);

    }

    public void Animate(
        CanvasGroup canvasGroup,
        float fadeInDuration)
    {
        fadeSequence = GetFadeInSequence(canvasGroup, fadeInDuration);
    }

    public void Animate(
        CanvasGroup canvasGroup,
        float waitDuration,
        float fadeOutDuration,
        Action onEnd)
    {
        fadeSequence = AppendFadeOutSequence(fadeSequence, canvasGroup, waitDuration, fadeOutDuration, onEnd);
    }

    private static Sequence GetFadeInSequence(CanvasGroup canvasGroup, float fadeInDuration)
    {
        return DOTween.Sequence()
                    .Append(canvasGroup.DOFade(1, fadeInDuration));
    }

    private static Sequence AppendFadeOutSequence(
        Sequence sequence, 
        CanvasGroup canvasGroup, 
        float waitDuration,
        float fadeOutDuration,
        Action onEnd)
    {
        return sequence
            .AppendInterval(waitDuration)
            .Append(canvasGroup.DOFade(0, fadeOutDuration))
            .AppendCallback(new TweenCallback(onEnd));
    }

    public void StopAnimating()
    {
        fadeSequence.Kill();
    }
}

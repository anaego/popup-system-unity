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
        fadeSequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1, fadeInDuration))
            .AppendInterval(waitDuration)
            .Append(canvasGroup.DOFade(0, fadeOutDuration))
            .AppendCallback(new TweenCallback(onEnd))
            .OnKill(() => canvasGroup.alpha = 1);
    }

    public void Animate(
        CanvasGroup canvasGroup,
        float fadeInDuration)
    {
        fadeSequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1, fadeInDuration))
            .OnKill(() => canvasGroup.alpha = 1);
    }

    public void Animate(
        CanvasGroup canvasGroup,
        float fadeOutDuration,
        Action onEnd)
    {
        StopAnimating();
        fadeSequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(0, fadeOutDuration))
            .AppendCallback(new TweenCallback(onEnd))
            .OnKill(() => canvasGroup.alpha = 0);
    }

    public void StopAnimating()
    {
        fadeSequence.Kill();
    }
}

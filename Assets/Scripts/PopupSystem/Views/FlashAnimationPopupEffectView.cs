using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FlashAnimationPopupEffectView : PopupEffectView
{
    [SerializeField] private Image image;

    private Sequence sequence;

    public override void Play()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }
        sequence?.Kill();
        sequence = DOTween.Sequence()
            .Append(image.DOFade(1, 0.5f))
            .Append(image.DOFade(0, 0.5f))
            .Play();
        Debug.Log("Playering");
    }
}

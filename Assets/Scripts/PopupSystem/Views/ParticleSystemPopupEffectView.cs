using UnityEngine;

public class ParticleSystemPopupEffectView : PopupEffectView
{
    [SerializeField] private ParticleSystem particles;

    public override void Play()
    {
        particles.Play();
    }
}
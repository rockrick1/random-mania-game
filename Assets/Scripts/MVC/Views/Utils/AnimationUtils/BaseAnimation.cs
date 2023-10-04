using DG.Tweening;
using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    [SerializeField] protected float duration;
    [SerializeField] protected float delay;
    [SerializeField] protected Ease ease;
    [SerializeField] protected bool playOnAwake;
    [SerializeField] protected bool overrideDestinationValue;

    protected Tween tween;
    
    void Awake ()
    {
        Setup();
        if (playOnAwake)
            Play();
    }

    public virtual void Play () => tween?.Kill();

    public void Kill () => tween?.Kill();

    protected abstract void Setup ();
}

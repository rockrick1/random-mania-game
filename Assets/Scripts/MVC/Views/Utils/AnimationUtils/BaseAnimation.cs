using DG.Tweening;
using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    [SerializeField] protected float duration;
    [SerializeField] protected float delay;
    [SerializeField] protected Ease ease;
    [SerializeField] protected bool playOnAwake;
    [SerializeField] protected bool overrideDestinationValue;
    
    void Awake ()
    {
        Setup();
        if (playOnAwake)
            Play();
    }

    public abstract void Play ();

    protected abstract void Setup ();
}

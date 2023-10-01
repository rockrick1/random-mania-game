using DG.Tweening;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] public Vector3 moveFrom;
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] Ease ease;
    [SerializeField] bool playOnAwake;

    Vector3 moveTo;

    RectTransform Transform => (RectTransform) transform;
    
    void Awake ()
    {
        Setup();
        if (playOnAwake)
            Play();
    }

    void Setup ()
    {
        moveTo = Transform.localPosition;
    }

    public void Play ()
    {
        if (moveTo == default)
            Setup();
        transform.DOLocalMove(moveFrom, 0);
        transform.DOLocalMove(moveTo, duration).SetDelay(delay).SetEase(ease);
    }
}
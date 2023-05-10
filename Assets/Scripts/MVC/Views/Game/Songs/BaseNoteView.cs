using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BaseNoteView : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Vector3 hitEndScale;
    [SerializeField] float hitAnimDuration;
    
    public Note Note { get; set; }
    public float Speed { get; set; }

    bool hit;

    public virtual void HitAnimation ()
    {
        hit = true;
        transform.DOScale(hitEndScale, hitAnimDuration).SetEase(Ease.OutCubic);
        image.DOFade(0f, hitAnimDuration).SetEase(Ease.OutQuad).OnComplete(OnHitAnimationEnd);
    }

    public void Destroy () => Destroy(gameObject);

    void OnHitAnimationEnd ()
    {
        hit = false;
        Destroy();
    }

    void Update ()
    {
        if (!hit && !GameManager.IsPaused)
            transform.localPosition += Vector3.down * (Speed * Time.deltaTime);
    }
}
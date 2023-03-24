using UnityEngine;

public class NoteView : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    static readonly int HitAnimationHash = Animator.StringToHash("hit");
    
    public Note Note { get; set; }
    public float Speed { get; set; }
    
    bool hit;

    public void HitAnimation ()
    {
        hit = true;
        animator.SetBool(HitAnimationHash, hit);
    }

    public void OnHitAnimationEnd ()
    {
        hit = false;
        animator.SetBool(HitAnimationHash, hit);
        Destroy();
    }

    public void Destroy () => Destroy(gameObject);

    void Update ()
    {
        if (!hit)
            transform.position += Vector3.down * (Speed * Time.deltaTime);
    }
}
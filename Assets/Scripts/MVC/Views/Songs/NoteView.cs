﻿using UnityEngine;

public class NoteView : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    static readonly int Hit = Animator.StringToHash("hit");
    
    public Note Note { get; set; }
    public float Speed { get; set; }

    public void HitAnimation ()
    {
        animator.SetBool(Hit, true);
    }

    public void Destroy () => Destroy(gameObject);

    void Update ()
    {
        transform.position += Vector3.down * (Speed * Time.deltaTime);
    }
}
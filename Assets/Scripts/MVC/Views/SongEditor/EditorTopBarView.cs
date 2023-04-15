using System;
using UnityEngine;

public class EditorTopBarView : MonoBehaviour
{
    public event Action OnClick;
    public event Action OnRelease;
    
    [SerializeField] AudioSource songPlayer;
    [SerializeField] RectTransform waveLine;
    [SerializeField] RectTransform lineParent;
    [SerializeField] UIClickHandler button;

    public float CurrentZoom => lineParent.localScale.x;

    float waveLineWidth;
    float progress;

    float startAnchor = .5f;
    float endAnchor = -.5f;

    void Start ()
    {
        waveLineWidth = lineParent.rect.width;
        button.OnLeftClick.AddListener(() => OnClick?.Invoke());
        button.OnLeftRelease.AddListener(() => OnRelease?.Invoke());
    }

    public void ChangeZoom (float amount)
    {
        Vector3 localScale = lineParent.localScale;
        float newScale = amount > 0 ? localScale.x * 1.1f : localScale.x * 0.9f;
        localScale = new Vector3(newScale, localScale.y);
        lineParent.localScale = localScale;
        waveLineWidth = lineParent.rect.width;
    }

    void Update ()
    {
        if (songPlayer.clip == null)
            return;
        progress = songPlayer.time / songPlayer.clip.length;
        waveLine.anchoredPosition = new Vector3(-progress * waveLineWidth, 0);
    }
}
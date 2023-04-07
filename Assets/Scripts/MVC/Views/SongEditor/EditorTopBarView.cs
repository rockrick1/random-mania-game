using UnityEngine;

public class EditorTopBarView : MonoBehaviour
{
    [SerializeField] AudioSource songPlayer;
    [SerializeField] RectTransform waveLine;
    [SerializeField] RectTransform lineParent;

    float waveLineWidth;
    float progress;

    float startAnchor = .5f;
    float endAnchor = -.5f;

    void Start ()
    {
        waveLineWidth = lineParent.rect.width;
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
        if (!songPlayer.isPlaying || songPlayer.clip == null)
            return;
        progress = songPlayer.time / songPlayer.clip.length;
        waveLine.anchoredPosition = new Vector3(-progress * waveLineWidth, 0);
    }
}
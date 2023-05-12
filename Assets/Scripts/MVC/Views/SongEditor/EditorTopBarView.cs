using UnityEngine;

public class EditorTopBarView : MonoBehaviour
{
    [SerializeField] AudioSource songPlayer;
    [SerializeField] RectTransform waveLine;
    [SerializeField] RectTransform lineParent;
    [SerializeField] EditorTopBarDragHandler dragHandler;

    public float CurrentZoom => lineParent.localScale.x;

    float progress;

    float startAnchor = .5f;
    float endAnchor = -.5f;

    void Start ()
    {
    }

    public void ChangeZoom (float amount)
    {
        Vector3 localScale = lineParent.localScale;
        float newScale = amount > 0 ? localScale.x * 1.1f : localScale.x * 0.9f;
        localScale = new Vector3(newScale, localScale.y);
        lineParent.localScale = localScale;
    }

    public void SetWaveActive (bool active) => waveLine.gameObject.SetActive(active);

    void Update ()
    {
        if (songPlayer.clip == null)
            return;
        if (dragHandler.IsDragging)
        {
            AudioClip clip = songPlayer.clip;
            songPlayer.time = Mathf.Clamp(dragHandler.Progress * clip.length, 0, clip.length - .1f);
        }
        else
        {
            progress = songPlayer.time / songPlayer.clip.length;
            dragHandler.SetProgress(progress);
        }
    }
}
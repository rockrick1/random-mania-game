using UnityEngine;

public class EditorTopBarView : MonoBehaviour
{
    [SerializeField] RectTransform waveLine;
    [SerializeField] RectTransform lineParent;
    [SerializeField] EditorTopBarDragHandler dragHandler;

    IAudioManager audioManager;
    float progress;

    void Start ()
    {
        audioManager = AudioManager.GetOrCreate();
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
        if (!audioManager.HasMusicClip)
            return;
        float musicLength = audioManager.MusicLength;
        if (!dragHandler.IsDragging)
        {
            progress = audioManager.MusicTime / musicLength;
            dragHandler.SetProgress(progress);
        }
        else
            audioManager.SetMusicTime(Mathf.Clamp(dragHandler.Progress * musicLength, 0, musicLength - .1f));
    }
}
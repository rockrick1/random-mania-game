using UnityEngine;

public class LongNoteView : BaseNoteView
{
    [SerializeField] NoteView upper;
    [SerializeField] NoteView lower;
    
    public override void HitAnimation () => Destroy();

    public void SetHeight (float height)
    {
        ((RectTransform) transform).sizeDelta = new Vector2(((RectTransform) transform).sizeDelta.x, height);
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpperSongView : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] NoteView noteViewPrefab;
    [SerializeField] LongNoteView longNoteViewPrefab;
    
    [SerializeField] Animator hitFeedbackAnimator;
    [SerializeField] GameObject perfectText;
    [SerializeField] GameObject greatText;
    [SerializeField] GameObject okayText;
    [SerializeField] GameObject missText;
    
    static readonly int hitAnimationHash = Animator.StringToHash("hit");

    public float SpawnerYPos => transform.localPosition.y - ((RectTransform)transform).sizeDelta.y / 2;

    public BaseNoteView SpawnNote (Note note, float noteSpeed)
    {
        NoteView instance = Instantiate(noteViewPrefab, spawnPoints[note.Position]);
        instance.Note = note;
        instance.Speed = noteSpeed;
        return instance;
    }
    
    public BaseNoteView SpawnLongNote (Note note, float noteSpeed, float noteHeight)
    {
        LongNoteView instance = Instantiate(longNoteViewPrefab, spawnPoints[note.Position]);
        instance.SetHeight(noteHeight);
        instance.Note = note;
        instance.Speed = noteSpeed;
        return instance;
    }

    public void ShowHitFeedback (HitScore score)
    {
        perfectText.SetActive(score == HitScore.Perfect);
        greatText.SetActive(score == HitScore.Great);
        okayText.SetActive(score == HitScore.Okay);
        missText.SetActive(score == HitScore.Miss);
        hitFeedbackAnimator.SetTrigger(hitAnimationHash);
    }
}
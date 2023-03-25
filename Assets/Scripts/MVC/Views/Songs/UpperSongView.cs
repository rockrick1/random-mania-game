using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpperSongView : MonoBehaviour
{
    [SerializeField] LowerSongView lowerSongView;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] NoteView noteViewPrefab;
    [SerializeField] Transform notesContainer;
    [SerializeField] TextMeshProUGUI hitFeedbackText;
    
    [SerializeField] Animator hitFeedbackAnimator;
    [SerializeField] GameObject perfectText;
    [SerializeField] GameObject greatText;
    [SerializeField] GameObject okayText;
    [SerializeField] GameObject missText;
    
    static readonly int hitAnimationHash = Animator.StringToHash("hit");

    public IReadOnlyList<Transform> SpawnPoints => spawnPoints;

    public void Initialize ()
    {
    }

    public NoteView SpawnNote (Note note, float noteSpeed)
    {
        NoteView instance = Instantiate(noteViewPrefab, spawnPoints[note.Position]);
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
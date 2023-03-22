using System.Collections.Generic;
using UnityEngine;

public class UpperSongView : MonoBehaviour
{
    [SerializeField] LowerSongView lowerSongView;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] NoteView noteViewPrefab;
    [SerializeField] Transform notesContainer;

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
}
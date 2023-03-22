using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UpperSongView : MonoBehaviour
{
    [SerializeField] LowerSongView lowerSongView;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] NoteView noteViewPrefab;
    [SerializeField] Transform notesContainer;

    public IReadOnlyList<Transform> SpawnPoints => spawnPoints;

    IObjectPool<NoteView> notePool;
    List<NoteView> liveNotes = new();
    float noteSpeed;

    public void Initialize ()
    {
    }

    public void SpawnNote (Note note)
    {
        NoteView instance = Instantiate(noteViewPrefab, spawnPoints[note.Position]);
        liveNotes.Add(instance);
    }

    public void SetApproachRate (float approachRate)
    {
        noteSpeed = (spawnPoints[0].transform.position.y - lowerSongView.HitterYPos) / approachRate;
    }

    void Update ()
    {
        for (int i = 0; i < liveNotes.Count;)
        {
            NoteView note = liveNotes[i];
            note.transform.position += Vector3.down * (noteSpeed * Time.deltaTime);
            if (note.transform.position.y < lowerSongView.HitterYPos)
            {
                liveNotes.Remove(note);
                Destroy(note.gameObject);
                // Debug.LogError("babooey?");
            }
            else
                i++;
        }
    }
}
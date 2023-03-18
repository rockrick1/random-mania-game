using System;
using System.Collections;
using UnityEngine;

public class SongModel : ISongModel
{
    const float HIT_WINDOW = 0.25f;

    readonly INoteSpawnerModel noteSpawnerModel;
    readonly ISongLoaderModel songLoaderModel;

    double elapsed;

    public SongModel (INoteSpawnerModel noteSpawnerModel, ISongLoaderModel songLoaderModel)
    {
        this.noteSpawnerModel = noteSpawnerModel;
        this.songLoaderModel = songLoaderModel;
    }

    public event Action<Note> OnNoteHit;
    public event Action<Note> OnNoteMissed;
    public event Action OnSongFinished;

    public ISongSettings CurrentSongSettings => songLoaderModel.Settings;
    public AudioClip CurrentSongAudio => songLoaderModel.Audio;

    public void Initialize ()
    {
        noteSpawnerModel.Initialize();
        songLoaderModel.Initialize();
    }

    public void LoadSong (string songId)
    {
        songLoaderModel.LoadSong(songId);
        noteSpawnerModel.SetSong(CurrentSongSettings);
    }

    public void Play ()
    {
        noteSpawnerModel.Play();
        CoroutineRunner.Instance.StartCoroutine(nameof(SongRoutine), SongRoutine());
    }

    public void Dispose ()
    {
    }

    IEnumerator SongRoutine ()
    {
        var notes = CurrentSongSettings.Notes;
        var hitWindow = TimeSpan.FromSeconds(HIT_WINDOW);
        var notesIndex = 0;
        while (true)
        {
            elapsed += Time.deltaTime;
            var noteDistance = TimeSpan.FromSeconds(notes[notesIndex].Timestamp - elapsed);
            if (noteDistance < hitWindow)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log($"note hit! {noteDistance.TotalMilliseconds}");
                    OnNoteHit?.Invoke(notes[notesIndex]);
                    if (++notesIndex >= notes.Count)
                        break;
                    continue;
                }

            if (noteDistance < -hitWindow)
            {
                Debug.Log("MISS! YOU SUCK");
                OnNoteMissed?.Invoke(notes[notesIndex]);
                if (++notesIndex >= notes.Count)
                    break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(hitWindow.Seconds * 3);
        OnSongFinished?.Invoke();
    }
}
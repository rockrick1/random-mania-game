using System;

public interface INoteSpawnerModel
{
    event Action<Note> OnNoteSpawned;
    
    void Initialize();
    void SetSong(ISongSettings currentSongSettings);
    void Play();
}
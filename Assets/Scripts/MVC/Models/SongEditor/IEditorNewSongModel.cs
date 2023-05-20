using System;

public interface IEditorNewSongModel : IDisposable
{
    string LastCreatedSongId { get; }
    
    void Initialize ();
    void CreateSong (string songName, string artistName);
    void OpenSongFolder ();
    bool SongExists (string songName, string artistName);
}
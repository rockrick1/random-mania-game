using System;

public interface IEditorNewSongModel : IDisposable
{
    string LastCreatedSongId { get; }
    
    void Initialize ();
    void CreateSongFolder (string songId);
    void OpenSongFolder ();
    bool SongExists (string songId);
}
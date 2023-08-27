using System;

public interface IEditorNewSongModel : IDisposable
{
    string LastCreatedSongId { get; }
    string LastCreatedSongDifficultyName { get; }

    void Initialize ();
    void CreateSong (string songName, string artistName, string songDifficultyName);
    void OpenSongFolder ();
    bool SongExists (string songName, string artistName);
}
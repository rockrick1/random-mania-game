using System;
using System.Collections.Generic;

public interface ISongMenuModel : IDisposable
{
    event Action<ISongSettings> OnSongSelected;
    
    ISongSettings SelectedSongSettings { get; }
    
    void Initialize ();
    IReadOnlyList<ISongSettings> GetAllSongs ();
    void PickFirstSong ();
    bool PickSong(string songId, string songDifficultyName);
    void EnterGame ();
}
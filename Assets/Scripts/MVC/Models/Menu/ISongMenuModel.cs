using System;
using System.Collections.Generic;

public interface ISongMenuModel : IDisposable
{
    event Action<string, string> OnSongSelected;
    
    void Initialize ();
    IReadOnlyList<ISongSettings> GetAllSongs ();
    void PickSong(string songId, string songDifficultyName);
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ISongLoaderModel
{
    event Action OnSongLoaded;
    event Action OnSongSaved;
    event Action OnSongCreated;
    
    Dictionary<string, Dictionary<string, ISongSettings>> SongsCache { get; }
    Dictionary<string, AudioClip> SongsAudioCache { get; }
    string SongsPath { get; }
    string SelectedSongId { get; set; }
    string SelectedSongDifficulty { get; set; }

    void Initialize ();
    ISongSettings GetSongSettings (string songId, string difficultyName);
    ISongSettings GetSelectedSongSettings ();
    Task<AudioClip> GetSongAudio (string songId);
    Task<AudioClip> GetSelectedSongAudio (Action<AudioClip> onFinish);
    void CreateSong (string songName, string artistName, string songDifficultyName);
    void SaveSong (ISongSettings settings);
    bool SongExists (string songName, string artistName);
    IReadOnlyList<ISongSettings> GetAllSongSettings ();
}
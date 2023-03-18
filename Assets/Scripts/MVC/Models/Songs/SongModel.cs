using UnityEngine;

public class SongModel : ISongModel
{
    public ISongSettings CurrentSongSettings => songLoaderModel.Settings;
    public AudioClip CurrentSongAudio => songLoaderModel.Audio;
    
    readonly INoteSpawnerModel noteSpawnerModel;
    readonly ISongLoaderModel songLoaderModel;


    public SongModel(INoteSpawnerModel noteSpawnerModel, ISongLoaderModel songLoaderModel)
    {
        this.noteSpawnerModel = noteSpawnerModel;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize()
    {
        noteSpawnerModel.Initialize();
        songLoaderModel.Initialize();
    }

    public void InitializeSong(string songId)
    {
        songLoaderModel.InitializeSong(songId);
    }

    public void Dispose()
    {
        
    }
}
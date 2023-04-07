using System.Collections.Generic;
using UnityEngine;

public class EditorSongModel : IEditorSongModel
{
    readonly IEditorInputManager inputManager;
    readonly ISongLoaderModel songLoaderModel;

    SongSettings currentSongSettings;
    
    public EditorSongModel (IEditorInputManager inputManager, ISongLoaderModel songLoaderModel)
    {
        this.inputManager = inputManager;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }
    
    void AddListeners ()
    {
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
        inputManager.OnSavePressed += HandleSavePressed;
    }

    void RemoveListeners ()
    {
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
        inputManager.OnSavePressed -= HandleSavePressed;
    }

    void HandleSongLoaded ()
    {
        currentSongSettings = songLoaderModel.Settings;
    }
    

    void HandleSavePressed ()
    {
        songLoaderModel.SaveSong(currentSongSettings);
    }
    

    public void ButtonClicked (int pos)
    {
        Debug.Log(pos);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
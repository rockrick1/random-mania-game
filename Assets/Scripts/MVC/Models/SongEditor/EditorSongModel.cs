using System.Collections.Generic;
using UnityEngine;

public class EditorSongModel : IEditorSongModel
{
    public int SelectedSignature { get; }
    
    readonly IEditorInputManager inputManager;
    readonly ISongLoaderModel songLoaderModel;

    readonly List<int> indexes1_1 = new() {1};
    readonly List<int> indexes1_2 = new() {1, 2};
    readonly List<int> indexes1_3 = new() {1, 3, 3};
    readonly List<int> indexes1_4 = new() {1, 4, 2, 4};
    readonly List<int> indexes1_6 = new() {1, 4, 3, 2, 3, 4};

    SongSettings currentSongSettings;
    
    public EditorSongModel (IEditorInputManager inputManager, ISongLoaderModel songLoaderModel)
    {
        this.inputManager = inputManager;
        this.songLoaderModel = songLoaderModel;
        SelectedSignature = 4;
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
        float y = inputManager.GetMousePos().y;
        Debug.Log(pos);
    }

    public int GetIntervalByIndex (int i, int signature)
    {
        switch (signature)
        {
            case 1:
                return indexes1_1[i % signature];
            case 2:
                return indexes1_2[i % signature];
            case 3:
                return indexes1_3[i % signature];
            case 4:
                return indexes1_4[i % signature];
            case 6:
                return indexes1_6[i % signature];
        }

        return 1;
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditorSongPickerController : IDisposable
{
    readonly EditorSongPickerView view;
    readonly EditorNewSongController newSongController;
    readonly IEditorSongPickerModel model;
    readonly SongLoader songLoader;

    public EditorSongPickerController (
        EditorSongPickerView view,
        EditorNewSongController newSongController,
        IEditorSongPickerModel model,
        SongLoader songLoader
    )
    {
        this.view = view;
        this.newSongController = newSongController;
        this.model = model;
        this.songLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
        RefreshOptions();
    }

    void AddListeners ()
    {
        newSongController.OnEditNewSong += HandleEditNewSong;
        songLoader.OnSongCreated += HandleSongCreated;
        view.OnSongPicked += HandleSongPicked;
        view.OnOpenFolderClicked += HandleOpenFolderClicked;
        view.OnNewSongClicked += HandleNewSongClicked;
        view.OnRefreshClicked += HandleRefreshClicked;
    }

    void RemoveListeners ()
    {
        newSongController.OnEditNewSong -= HandleEditNewSong;
        songLoader.OnSongCreated -= HandleSongCreated;
        view.OnSongPicked -= HandleSongPicked;
        view.OnOpenFolderClicked -= HandleOpenFolderClicked;
        view.OnNewSongClicked -= HandleNewSongClicked;
        view.OnRefreshClicked -= HandleRefreshClicked;
    }

    void HandleEditNewSong (string songId, string songDifficultyName)
    {
        view.PickSong(songId);
        model.PickSong(songId, songDifficultyName);
    }

    void HandleSongCreated () => RefreshOptions();

    void HandleSongPicked (string songLabel) => model.PickSong(songLabel);

    void HandleOpenFolderClicked () => Application.OpenURL($"file://{songLoader.SongsPath}");

    void HandleNewSongClicked () => newSongController.Open();

    void HandleRefreshClicked () => RefreshOptions();

    void RefreshOptions ()
    {
        List<string> options = new();
        foreach (var songId in songLoader.SongsCache.Keys)
            foreach (var difficultyName in songLoader.SongsCache[songId].Keys)
                options.Add($"{songId} [{difficultyName}]");
        view.LoadOptions(options);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
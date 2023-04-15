﻿using System;
using UnityEngine;

public class EditorController : IDisposable
{
    readonly EditorSongPickerController songPickerController;
    readonly EditorSongDetailsController songDetailsController;
    readonly EditorTopBarController topBarController;
    readonly EditorSongController songController;
    readonly EditorView view;
    readonly IEditorModel model;
    readonly IEditorSongModel songModel;
    readonly IEditorInputManager inputManager;

    public EditorController (
        EditorSongPickerController songPickerController,
        EditorSongDetailsController songDetailsController,
        EditorTopBarController topBarController,
        EditorSongController songController,
        EditorView view,
        IEditorModel model,
        IEditorSongModel songModel,
        IEditorInputManager inputManager
    )
    {
        this.songPickerController = songPickerController;
        this.songDetailsController = songDetailsController;
        this.topBarController = topBarController;
        this.songController = songController;
        this.view = view;
        this.model = model;
        this.songModel = songModel;
        this.inputManager = inputManager;
    }

    public void Initialize ()
    {
        songPickerController.Initialize();
        songDetailsController.Initialize();
        topBarController.Initialize();
        songController.Initialize();
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnSongLoaded += HandleSongLoaded;
        inputManager.OnSongPlayPause += HandlePlayPause;
        inputManager.OnSongScroll += HandleSongScroll;
    }

    void RemoveListeners ()
    {
        model.OnSongLoaded -= HandleSongLoaded;
        inputManager.OnSongPlayPause -= HandlePlayPause;
        inputManager.OnSongScroll -= HandleSongScroll;
    }

    void HandleSongLoaded (AudioClip clip, ISongSettings settings)
    {
        view.SetSong(clip);
        view.WaveForm2D.SetAudio(clip);
        view.WaveForm2D.ShowWave();
        model.ProcessSong(clip);
        view.ClearSeparators();
    }

    void HandlePlayPause () => view.PlayPauseSong();

    void HandleSongScroll (float amount)
    {
        view.SetSongTime(songModel.GetNextBeat(view.SongPlayer.time, Mathf.RoundToInt(-amount)));
    }

    public void Dispose ()
    {
        RemoveListeners();
        songPickerController.Dispose();
        songDetailsController.Dispose();
        topBarController.Dispose();
        songController.Dispose();
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class EditorSongPickerController : IDisposable
{
    readonly EditorSongPickerView view;
    readonly IEditorSongPickerModel model;

    public EditorSongPickerController (EditorSongPickerView view, IEditorSongPickerModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize ()
    {
        AddListeners();
        view.LoadOptions(new List<String>{"TearRain", "Unknown"});
    }

    void AddListeners ()
    {
        view.OnSongPicked += HandleSongPicked;
    }

    void RemoveListeners ()
    {
        view.OnSongPicked -= HandleSongPicked;
    }

    void HandleSongPicked (string song)
    {
        model.PickSong(song);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
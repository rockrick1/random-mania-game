using System;
using System.Collections.Generic;
using UnityEngine;

public class EditorSongController : IDisposable
{
    readonly EditorSongView view;
    readonly EditorSongDetailsController songDetailsController;
    readonly IEditorSongModel model;
    readonly ISongLoaderModel songLoaderModel;
    readonly IAudioManager audioManager;
    readonly IEditorInputManager inputManager;
    
    List<BaseNoteView> activeNoteViews = new();

    public EditorSongController (
        EditorSongView view,
        EditorSongDetailsController songDetailsController,
        IEditorSongModel model,
        ISongLoaderModel songLoaderModel,
        IAudioManager audioManager,
        IEditorInputManager inputManager
    )
    {
        this.view = view;
        this.songDetailsController = songDetailsController;
        this.model = model;
        this.songLoaderModel = songLoaderModel;
        this.audioManager = audioManager;
        this.inputManager = inputManager;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnFieldButtonLeftClicked += HandleFieldButtonLeftClicked;
        view.OnFieldButtonLeftReleased += HandleFieldButtonLeftReleased;
        songDetailsController.OnApplyClicked += HandleApplySongDetails;
        songDetailsController.OnSignatureChanged += HandleSignatureChanged;
        songDetailsController.OnPlaybackSpeedChanged += HandlePlaybackSpeedChanged;
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
        inputManager.OnSongPlayPause += HandlePlayPause;
        inputManager.OnSongScroll += HandleSongScroll;
    }

    void RemoveListeners ()
    {
        view.OnFieldButtonLeftClicked -= HandleFieldButtonLeftClicked;
        view.OnFieldButtonLeftReleased -= HandleFieldButtonLeftReleased;
        songDetailsController.OnApplyClicked -= HandleApplySongDetails;
        songDetailsController.OnSignatureChanged -= HandleSignatureChanged;
        songDetailsController.OnPlaybackSpeedChanged -= HandlePlaybackSpeedChanged;
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
        inputManager.OnSongPlayPause -= HandlePlayPause;
        inputManager.OnSongScroll -= HandleSongScroll;
    }

    void HandleFieldButtonLeftClicked (int pos) => model.StartCreatingNote(pos, audioManager.MusicTime, view.Height);

    void HandleFieldButtonLeftReleased (int pos)
    {
        NoteCreationResult? result = model.CreateNote(pos, audioManager.MusicTime, view.Height);
        if (!result.HasValue)
            return;
        foreach (int i in result.Value.Substituted)
            RemoveNoteViewAt(i);

        if (result.Value.Note.IsLong)
        {
            EditorLongNoteView noteView = view.CreateLongNote(result.Value.Note);
            AddLongNoteViewAt(noteView, result.Value.Index);
        }
        else
        {
            EditorNoteView noteView = view.CreateNote(result.Value.Note);
            AddNoteViewAt(noteView, result.Value.Index);
        }
    }

    void HandleApplySongDetails (float bpm, float ar, float diff, float startingTime)
    {
        model.ChangeBpm(bpm);
        model.ChangeAr(ar);
        model.ChangeDiff(diff);
        model.ChangeStartingTime(startingTime);
        HandleSongLoaded();
    }

    void HandleSignatureChanged (int signature)
    {
        if (model.SelectedSignature == signature)
            return;
        model.ChangeSignature(signature);
        view.ClearSeparators();
        CreateHorizontalSeparators();
    }

    void HandlePlaybackSpeedChanged (float speed)
    {
        audioManager.SetMusicPlaybackSpeed(speed);
    }

    void HandleSongLoaded ()
    {
        ClearNotes();
        view.ClearSeparators();
        view.SetupSong(songLoaderModel.Settings, songLoaderModel.Audio.length);
        CreateNotes();
        CreateHorizontalSeparators();
    }

    void CreateNotes ()
    {
        for (int i = 0; i < songLoaderModel.Settings.Notes.Count; i++)
        {
            Note note = songLoaderModel.Settings.Notes[i];
            if (note.IsLong)
                AddLongNoteViewAt(view.CreateLongNote(note), i);
            else
                AddNoteViewAt(view.CreateNote(note), i);
        }
    }

    void ClearNotes ()
    {
        foreach (BaseNoteView noteView in activeNoteViews)
            noteView.Destroy();
        activeNoteViews.Clear();
    }

    void AddNoteViewAt (EditorNoteView noteView, int index)
    {
        noteView.OnRightClick += HandleNoteRightClick;
        activeNoteViews.Insert(index, noteView);
    }

    void AddLongNoteViewAt (EditorLongNoteView noteView, int index)
    {
        noteView.OnLowerRightClick += HandleNoteRightClick;
        noteView.OnUpperRightClick += HandleNoteUpperRightClick;
        activeNoteViews.Insert(index, noteView);
    }

    void HandleNoteRightClick (BaseNoteView note)
    {
        int index = activeNoteViews.FindIndex(x => x == note);
        model.RemoveNoteAt(index);
        RemoveNoteViewAt(index);
    }

    void HandleNoteUpperRightClick (EditorLongNoteView note)
    {
        //TODO transform long note to single note
    }

    void HandlePlayPause () => audioManager.PlayPauseMusic();

    void HandleSongScroll (float amount) =>
        audioManager.SetMusicTime(model.GetNextBeat(audioManager.MusicTime, Mathf.RoundToInt(-amount)));

    void RemoveNoteViewAt (int index)
    {
        activeNoteViews[index].Destroy();
        activeNoteViews.RemoveAt(index);
    }

    void CreateHorizontalSeparators ()
    {
        int i = 0;
        for (float t = songLoaderModel.Settings.StartingTime;
             t < songLoaderModel.Audio.length;
             t += model.SignedBeatInterval, i++)
        {
            view.CreateSeparator(model.GetSeparatorColorByIndex(i));
        }
        view.ChangeSeparatorsDistance(model.SelectedSignature);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
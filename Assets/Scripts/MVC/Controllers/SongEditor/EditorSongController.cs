using System;
using System.Collections.Generic;

public class EditorSongController : IDisposable
{
    readonly EditorSongView view;
    readonly EditorSongDetailsController songDetailsController;
    readonly IEditorSongModel model;
    readonly ISongLoaderModel songLoaderModel;
    
    List<BaseNoteView> activeNoteViews = new();

    public EditorSongController (
        EditorSongView view,
        EditorSongDetailsController songDetailsController,
        IEditorSongModel model,
        ISongLoaderModel songLoaderModel
    )
    {
        this.view = view;
        this.songDetailsController = songDetailsController;
        this.model = model;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnFieldButtonLeftClicked += HandleFieldButtonLeftClicked;
        songDetailsController.OnApplyClicked += HandleApplySongDetails;
        songDetailsController.OnSignatureChanged += HandleSignatureChanged;
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
    }

    void RemoveListeners ()
    {
        view.OnFieldButtonLeftClicked -= HandleFieldButtonLeftClicked;
        songDetailsController.OnApplyClicked -= HandleApplySongDetails;
        songDetailsController.OnSignatureChanged -= HandleSignatureChanged;
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
    }

    void HandleFieldButtonLeftClicked (int pos)
    {
        NoteCreationResult? result = model.CreateNote(pos, view.SongPlayer.time, view.Height);
        if (!result.HasValue)
            return;
        if (result.Value.Substituted)
        {
            RemoveNoteViewAt(result.Value.Index);
        }
        //TODO separate long notes here
        EditorNoteView noteView = view.CreateNote(result.Value.Note);
        AddNoteViewAt(noteView, result.Value.Index);
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
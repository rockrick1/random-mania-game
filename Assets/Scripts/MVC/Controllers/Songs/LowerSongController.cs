using System;

public class LowerSongController : IDisposable
{
    readonly IGameInputManager inputManager;
    readonly ISongModel songModel;
    readonly LowerSongView view;

    public float HitterYPos => view.HitterYPos;

    int selectedHitter = 1;

    public LowerSongController (LowerSongView view, IGameInputManager inputManager, ISongModel songModel)
    {
        this.inputManager = inputManager;
        this.songModel = songModel;
        this.view = view;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        inputManager.OnHitterSelect += HandleHitterSelect;
        inputManager.OnHitterPress += HandleHitterPressed;
        inputManager.OnHitterRelease += HandleHitterReleased;
        songModel.OnNoteHit += HandleNoteHit;
        songModel.OnLongNoteHit += HandleLongNoteHit;
        songModel.OnLongNoteReleased += HandleLongNoteReleased;
        songModel.OnSongStarted += HandleSongStarted;
    }

    void RemoveListeners ()
    {
        inputManager.OnHitterSelect -= HandleHitterSelect;
        inputManager.OnHitterPress -= HandleHitterPressed;
        inputManager.OnHitterRelease -= HandleHitterReleased;
        songModel.OnNoteHit -= HandleNoteHit;
        songModel.OnLongNoteHit -= HandleLongNoteHit;
        songModel.OnLongNoteReleased -= HandleLongNoteReleased;
        songModel.OnSongStarted -= HandleSongStarted;
    }

    void HandleHitterSelect (int index)
    {
        if (index == selectedHitter)
            return;
        selectedHitter = index;
        view.SetActiveHitter(index);
    }

    void HandleHitterPressed(int index) => view.PlayHitterPressed(index);

    void HandleHitterReleased(int index) => view.PlayHitterReleased(index);

    void HandleNoteHit(Note note, double _) => view.PlayHitterEffect(note.Position);

    void HandleLongNoteHit(Note note, double _) => view.StartLongNoteEffect(note.Position);

    void HandleLongNoteReleased(Note note, double _) => view.EndLongNoteEffect(note.Position);

    void HandleSongStarted ()
    {
        selectedHitter = 1;
        view.SetActiveHitter(selectedHitter);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
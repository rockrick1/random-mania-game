using System;

public class LowerSongController : IDisposable
{
    readonly IGameInputManager inputManager;
    readonly ISongModel songModel;
    readonly LowerSongView view;

    public float HitterYPos => view.HitterYPos;

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
    }

    void RemoveListeners ()
    {
        inputManager.OnHitterSelect -= HandleHitterSelect;
        inputManager.OnHitterPress -= HandleHitterPressed;
        inputManager.OnHitterRelease -= HandleHitterReleased;
        songModel.OnNoteHit -= HandleNoteHit;
        songModel.OnLongNoteHit -= HandleLongNoteHit;
        songModel.OnLongNoteReleased -= HandleLongNoteReleased;
    }

    void HandleHitterSelect (int index) => view.SetActiveHitter(index);

    void HandleHitterPressed(int index) => view.PlayHitterPressed(index);

    void HandleHitterReleased(int index) => view.PlayHitterReleased(index);

    void HandleNoteHit(Note note, HitScore _) => view.PlayHitterEffect(note.Position);

    void HandleLongNoteHit(Note note, HitScore _) => view.StartLongNoteEffect(note.Position);

    void HandleLongNoteReleased(Note note, HitScore _) => view.EndLongNoteEffect(note.Position);

    public void Dispose ()
    {
        RemoveListeners();
    }
}
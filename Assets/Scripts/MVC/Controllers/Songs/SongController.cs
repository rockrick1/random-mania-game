public class SongController
{
    readonly ISongModel model;
    readonly SongView view;

    public SongController (SongView view, ISongModel model, LowerSongController lowerSongController)
    {
        this.view = view;
        this.model = model;
        LowerSongController = lowerSongController;
    }

    public NoteSpawnerController NoteSpawnerController { get; }
    public LowerSongController LowerSongController { get; }

    public void Initialize ()
    {
        LowerSongController.Initialize();
        view.SetClip(model.CurrentSongAudio);
        view.Play();
        model.Play();
    }
}
public class SongController
{
    public UpperSongController UpperSongController { get; }
    public LowerSongController LowerSongController { get; }
    
    readonly ISongModel model;
    readonly SongView view;

    public SongController (
        SongView view,
        ISongModel model,
        UpperSongController upperSongController,
        LowerSongController lowerSongController
    )
    {
        this.view = view;
        this.model = model;
        UpperSongController = upperSongController;
        LowerSongController = lowerSongController;
    }

    public void Initialize ()
    {
        UpperSongController.Initialize();
        LowerSongController.Initialize();
        view.SetClip(model.CurrentSongAudio);
        view.Play();
        model.Play();
    }
}
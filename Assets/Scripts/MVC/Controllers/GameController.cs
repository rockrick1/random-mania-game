using System;

public class GameController : IDisposable
{
    public GameController (SongController songController)
    {
        SongController = songController;
    }

    public SongController SongController { get; }

    public void Dispose ()
    {
    }

    public void Initialize ()
    {
        SongController.Initialize();
    }
}
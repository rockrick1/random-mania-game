using System;

public class GameController : IDisposable
{
    public SongController SongController { get; }
    
    public GameController (SongController songController)
    {
        SongController = songController;
    }

    public void Initialize ()
    {
        SongController.Initialize();
    }

    public void Dispose ()
    {
    }
}
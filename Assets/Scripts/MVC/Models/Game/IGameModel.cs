using System;

public interface IGameModel : IDisposable
{
    ISongModel SongModel { get; }
    IGameInputManager InputManager { get; }
    IAudioManager AudioManager { get; }

    void Initialize ();
}
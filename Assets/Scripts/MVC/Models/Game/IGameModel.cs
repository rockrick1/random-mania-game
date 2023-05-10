using System;

public interface IGameModel : IDisposable
{
    ISongModel SongModel { get; }
    IPauseModel PauseModel { get; }
    IGameInputManager InputManager { get; }
    IAudioManager AudioManager { get; }

    void Initialize ();
}
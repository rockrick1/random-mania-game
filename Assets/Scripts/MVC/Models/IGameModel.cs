public interface IGameModel
{
    ISongModel SongModel { get; }
    IGameInputManager InputManager { get; }
    IAudioManager AudioManager { get; }

    void Initialize ();
    void Dispose ();
}
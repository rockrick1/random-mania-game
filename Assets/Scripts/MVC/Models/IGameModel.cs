public interface IGameModel
{
    ISongModel SongModel { get; }
    IInputManager InputManager { get; }

    void Initialize ();
    void Dispose ();
}
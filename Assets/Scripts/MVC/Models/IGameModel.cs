public interface IGameModel
{
    ISongModel SongModel { get; }
    IGameInputManager InputManager { get; }

    void Initialize ();
    void Dispose ();
}
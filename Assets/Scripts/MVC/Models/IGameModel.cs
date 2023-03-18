public interface IGameModel
{
    ISongModel SongModel { get; }
    
    void Initialize();
    void Dispose();
}
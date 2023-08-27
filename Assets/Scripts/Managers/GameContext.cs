public class GameContext
{
    static GameContext instance;
    public static GameContext Current => instance ??= new GameContext();
    
    public string SelectedSongId { get; set; }
    public string SelectedSongDifficulty { get; set; }
}
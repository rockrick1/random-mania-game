using UnityEngine;

public class GameContext
{
    const string APPROACH_RATE = "approachRate";
    
    static GameContext instance;
    public static GameContext Current => instance ??= new GameContext();
    
    public string SelectedSongId { get; set; }
    public string SelectedSongDifficulty { get; set; }

    public float ApproachRate
    {
        get => PlayerPrefs.GetFloat(APPROACH_RATE);
        set => PlayerPrefs.SetFloat(APPROACH_RATE, value);
    }
}
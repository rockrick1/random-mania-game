using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] SongView songView;
    [SerializeField] ComboView comboView;
    [SerializeField] PauseView pauseView;
    [SerializeField] ScoreView scoreView;
    [SerializeField] ResultsView resultsView;
    [SerializeField] GameBackgroundView backgroundView;
    [SerializeField] SkipSongStartView skipSongStartView;

    public SongView SongView => songView;
    public ComboView ComboView => comboView;
    public PauseView PauseView => pauseView;
    public ScoreView ScoreView => scoreView;
    public ResultsView ResultsView => resultsView;
    public GameBackgroundView GameBackgroundView => backgroundView;
    public SkipSongStartView SkipSongStartView => skipSongStartView;
}
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] SongView songView;
    [SerializeField] ComboView comboView;
    [SerializeField] PauseView pauseView;

    public SongView SongView => songView;
    public ComboView ComboView => comboView;
    public PauseView PauseView => pauseView;
}
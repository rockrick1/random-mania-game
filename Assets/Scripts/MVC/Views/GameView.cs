using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] SongView songView;
    [SerializeField] ComboView comboView;

    public SongView SongView => songView;
    public ComboView ComboView => comboView;
}
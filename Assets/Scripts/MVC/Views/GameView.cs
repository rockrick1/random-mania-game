using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] SongView songView;

    public SongView SongView => songView;
}

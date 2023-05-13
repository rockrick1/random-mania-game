using UnityEngine;

public class SongView : MonoBehaviour
{
    [SerializeField] UpperSongView upperSongView;
    [SerializeField] LowerSongView lowerSongView;

    public UpperSongView UpperSongView => upperSongView;
    public LowerSongView LowerSongView => lowerSongView;
}
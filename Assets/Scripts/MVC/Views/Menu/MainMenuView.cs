using System;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    public event Action OnOpenSongMenu;
    public event Action OnOpenSettings;
    public event Action OnQuit;

    [SerializeField] UIClickHandler songMenuButton;
    [SerializeField] UIClickHandler settingsButton;
    [SerializeField] UIClickHandler quitButton;

    void Start ()
    {
        songMenuButton.OnLeftClick.AddListener(() => OnOpenSongMenu?.Invoke());
        settingsButton.OnLeftClick.AddListener(() => OnOpenSettings?.Invoke());
        quitButton.OnLeftClick.AddListener(() => OnQuit?.Invoke());
    }
}
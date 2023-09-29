using System;
using DG.Tweening;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    public event Action OnOpenSongMenu;
    public event Action OnOpenEditor;
    public event Action OnOpenSettings;
    public event Action OnQuit;

    [SerializeField] UIClickHandler songMenuButton;
    [SerializeField] UIClickHandler editorButton;
    [SerializeField] UIClickHandler settingsButton;
    [SerializeField] UIClickHandler quitButton;
    
    void Start ()
    {
        songMenuButton.OnLeftClick.AddListener(() => OnOpenSongMenu?.Invoke());
        editorButton.OnLeftClick.AddListener(() => OnOpenEditor?.Invoke());
        settingsButton.OnLeftClick.AddListener(() => OnOpenSettings?.Invoke());
        quitButton.OnLeftClick.AddListener(() => OnQuit?.Invoke());
    }

    public void AnimateScale(float scale, float duration) =>
        transform.DOScale(new Vector3(scale, scale, 1), duration).SetEase(Ease.InOutCubic);
}
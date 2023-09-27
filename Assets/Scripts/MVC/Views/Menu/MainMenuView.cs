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
    
    [Header("UIElements")]
    [SerializeField] Transform title;
    [SerializeField] Transform titleShadow;
    [SerializeField] float buttonAnimInterval;
    
    float titleInitialPos;
    float titleShadowInitialPos;
    float menuButton1InitialPos;
    float menuButton2InitialPos;
    float menuButton3InitialPos;
    float menuButton4InitialPos;

    float moveTime = .8f;
    
    void Start ()
    {
        songMenuButton.OnLeftClick.AddListener(() => OnOpenSongMenu?.Invoke());
        editorButton.OnLeftClick.AddListener(() => OnOpenEditor?.Invoke());
        settingsButton.OnLeftClick.AddListener(() => OnOpenSettings?.Invoke());
        quitButton.OnLeftClick.AddListener(() => OnQuit?.Invoke());
        
        titleInitialPos = title.localPosition.x;
        titleShadowInitialPos = titleShadow.localPosition.x;
        menuButton1InitialPos = songMenuButton.transform.localPosition.x;
        menuButton2InitialPos = editorButton.transform.localPosition.x;
        menuButton3InitialPos = settingsButton.transform.localPosition.x;
        menuButton4InitialPos = quitButton.transform.localPosition.x;

        PlayAnimations();
    }

    public void AnimateScale(float scale, float duration) =>
        transform.DOScale(new Vector3(scale, scale, 1), duration).SetEase(Ease.InOutCubic);

    void PlayAnimations()
    {
        title.DOLocalMoveX(-2600, 0);
        title.DOLocalMoveX(titleInitialPos, moveTime).SetEase(Ease.OutBack).SetDelay(.5f);
        titleShadow.DOLocalMoveX(-2600, 0);
        titleShadow.DOLocalMoveX(titleShadowInitialPos, moveTime).SetEase(Ease.OutBack).SetDelay(.6f);
        
        songMenuButton.transform.DOLocalMoveX(600, 0);
        songMenuButton.transform.DOLocalMoveX(menuButton1InitialPos, moveTime).SetEase(Ease.OutBack).SetDelay((buttonAnimInterval) + .5f);
        
        editorButton.transform.DOLocalMoveX(600, 0);
        editorButton.transform.DOLocalMoveX(menuButton2InitialPos, moveTime).SetEase(Ease.OutBack).SetDelay((buttonAnimInterval * 2) + .5f);
        
        settingsButton.transform.DOLocalMoveX(600, 0);
        settingsButton.transform.DOLocalMoveX(menuButton3InitialPos, moveTime).SetEase(Ease.OutBack).SetDelay((buttonAnimInterval * 3) + .5f);
        
        quitButton.transform.DOLocalMoveX(600, 0);
        quitButton.transform.DOLocalMoveX(menuButton4InitialPos, moveTime).SetEase(Ease.OutBack).SetDelay((buttonAnimInterval * 4) + .5f);
    }
}
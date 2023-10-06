using System;
using DG.Tweening;

public class MainMenuButtonsController : IDisposable
{
    const float INITIAL_X_POS = 229.45f;
    const float MOVE_X_AMOUNT = -100;
    const float MOVE_X_DURATION = .3f;
    
    readonly MainMenuView view;

    bool active;
    
    public MainMenuButtonsController (MainMenuView view)
    {
        this.view = view;
    }

    public void Initialize ()
    {
        active = true;
        AddListeners();
    }

    public void SetActive (bool active) => this.active = active;

    void AddListeners ()
    {
        view.SongMenuButton.OnHover.AddListener(() => HandleButtonHovered(view.SongMenuButton));
        view.SongMenuButton.OnUnhover.AddListener(() => HandleButtonUnhovered(view.SongMenuButton));
        view.EditorButton.OnHover.AddListener(() => HandleButtonHovered(view.EditorButton));
        view.EditorButton.OnUnhover.AddListener(() => HandleButtonUnhovered(view.EditorButton));
        view.SettingsButton.OnHover.AddListener(() => HandleButtonHovered(view.SettingsButton));
        view.SettingsButton.OnUnhover.AddListener(() => HandleButtonUnhovered(view.SettingsButton));
        view.QuitButton.OnHover.AddListener(() => HandleButtonHovered(view.QuitButton));
        view.QuitButton.OnUnhover.AddListener(() => HandleButtonUnhovered(view.QuitButton));
    }

    void HandleButtonHovered (UIClickHandler button)
    {
        if (!active)
            return;
        button.transform.DOLocalMoveX(INITIAL_X_POS + MOVE_X_AMOUNT, MOVE_X_DURATION).SetEase(Ease.OutCubic);
    }

    void HandleButtonUnhovered (UIClickHandler button)
    {
        if (!active)
            return;
        button.transform.DOLocalMoveX(INITIAL_X_POS, MOVE_X_DURATION).SetEase(Ease.OutCubic);
    }

    void RemoveListeners ()
    {
        view.SongMenuButton.OnHover.RemoveAllListeners();
        view.SongMenuButton.OnUnhover.RemoveAllListeners();
        view.EditorButton.OnHover.RemoveAllListeners();
        view.EditorButton.OnUnhover.RemoveAllListeners();
        view.SettingsButton.OnHover.RemoveAllListeners();
        view.SettingsButton.OnUnhover.RemoveAllListeners();
        view.QuitButton.OnHover.RemoveAllListeners();
        view.QuitButton.OnUnhover.RemoveAllListeners();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}
using System;

public class MainMenuController : IDisposable
{
    public event Action OnOpenSongMenu;
    public event Action OnOpenSettings;
    public event Action OnQuit;
    
    readonly MainMenuView view;
    readonly IMainMenuModel model;
    
    public MainMenuController (MainMenuView view, IMainMenuModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnOpenSongMenu += HandleOpenSongMenu;
        view.OnOpenSettings += HandleOpenSettings;
        view.OnQuit += HandleQuit;
    }

    void RemoveListeners ()
    {
        view.OnOpenSongMenu -= HandleOpenSongMenu;
        view.OnOpenSettings -= HandleOpenSettings;
        view.OnQuit -= HandleQuit;
    }

    void HandleOpenSongMenu () => OnOpenSongMenu?.Invoke();

    void HandleOpenSettings () => OnOpenSettings?.Invoke();
    
    void HandleQuit () => OnQuit?.Invoke();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}
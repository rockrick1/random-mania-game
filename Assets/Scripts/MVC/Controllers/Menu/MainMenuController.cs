using System;

public class MainMenuController : IDisposable
{
    public event Action OnOpenSongMenu;
    public event Action OnOpenEditor;
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
        view.OnOpenEditor += HandleOpenEditor;
        view.OnOpenSettings += HandleOpenSettings;
        view.OnQuit += HandleQuit;
    }

    void RemoveListeners ()
    {
        view.OnOpenSongMenu -= HandleOpenSongMenu;
        view.OnOpenEditor -= HandleOpenEditor;
        view.OnOpenSettings -= HandleOpenSettings;
        view.OnQuit -= HandleQuit;
    }

    void HandleOpenSongMenu () => OnOpenSongMenu?.Invoke();

    void HandleOpenEditor () => OnOpenEditor?.Invoke();

    void HandleOpenSettings () => OnOpenSettings?.Invoke();
    
    void HandleQuit () => OnQuit?.Invoke();

    public void ZoomIn() => view.AnimateScale(5f, 1f);
    
    public void ZoomOut() => view.AnimateScale(1f, 1f);

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}
using System;

public class MainMenuController : IDisposable
{
    public event Action OnOpenSongMenu;
    public event Action OnOpenEditor;
    public event Action OnOpenSettings;
    public event Action OnQuit;
    
    readonly MainMenuView view;
    readonly IMainMenuModel model;
    readonly MenuAnimationsController menuAnimationsController;
    
    public MainMenuController (MainMenuView view, IMainMenuModel model)
    {
        this.view = view;
        this.model = model;
        menuAnimationsController = new MenuAnimationsController(view.transform);
    }

    public void Initialize ()
    {
        AddListeners();
        menuAnimationsController.Initialize();
        menuAnimationsController.PlayOpen();
    }

    public void Open () => menuAnimationsController.PlayOpen();

    public void Close() => menuAnimationsController.PlayClose();

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

    void HandleOpenSongMenu ()
    {
        menuAnimationsController.PlayClose();
        OnOpenSongMenu?.Invoke();
    }

    void HandleOpenEditor () => OnOpenEditor?.Invoke();

    void HandleOpenSettings () => OnOpenSettings?.Invoke();
    
    void HandleQuit () => OnQuit?.Invoke();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}
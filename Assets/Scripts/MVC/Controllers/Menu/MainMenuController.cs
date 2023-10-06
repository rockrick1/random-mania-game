using System;
using UnityEngine.Device;

public class MainMenuController : IDisposable
{
    public event Action OnOpenSongMenu;
    public event Action OnOpenEditor;
    public event Action OnOpenSettings;
    public event Action OnQuit;
    
    readonly MainMenuView view;
    readonly IMainMenuModel model;
    readonly MenuAnimationsController menuAnimationsController;
    readonly MainMenuButtonsController buttonsController;
    
    public MainMenuController (
        MainMenuView view,
        IMainMenuModel model
    )
    {
        this.view = view;
        this.model = model;
        menuAnimationsController = new MenuAnimationsController(view.transform);
        buttonsController = new MainMenuButtonsController(view);
    }

    public void Initialize ()
    {
        AddListeners();
        
        menuAnimationsController.Initialize();
        buttonsController.Initialize();
        
        menuAnimationsController.PlayOpen();
    }

    public void Open ()
    {
        buttonsController.SetActive(true);
        menuAnimationsController.PlayOpen();
    }

    public void Close()
    {
        buttonsController.SetActive(false);
        menuAnimationsController.PlayClose();
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

    public void Dispose ()
    {
        model.Dispose();
        buttonsController.Dispose();
        RemoveListeners();
    }
}
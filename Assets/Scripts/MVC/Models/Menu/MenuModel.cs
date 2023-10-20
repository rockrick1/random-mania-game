using System;

public class MenuModel : IMenuModel
{
    public event Action OnBackPressed;
    
    public IMainMenuModel MainMenuModel { get; }
    public ISongMenuModel SongMenuModel { get; }
    public SongLoader SongLoader { get; }
    public IMenuInputManager InputManager { get; }
    public IAudioManager AudioManager { get; }
    
    public MenuModel (
        IMainMenuModel mainMenuModel,
        ISongMenuModel songMenuModel,
        SongLoader songLoader,
        IMenuInputManager inputManager,
        IAudioManager audioManager
    )
    {
        MainMenuModel = mainMenuModel;
        SongMenuModel = songMenuModel;
        SongLoader = songLoader;
        InputManager = inputManager;
        AudioManager = audioManager;
    }

    public void Initialize ()
    {
        AddListeners();
        MainMenuModel.Initialize();
        SongMenuModel.Initialize();
    }

    void AddListeners ()
    {
        InputManager.OnBackPressed += HandleBackPressed;
    }

    void RemoveListeners ()
    {
        InputManager.OnBackPressed -= HandleBackPressed;
    }

    void HandleBackPressed () => OnBackPressed?.Invoke();

    public void Dispose ()
    {
        RemoveListeners();
        MainMenuModel.Dispose();
        SongMenuModel.Dispose();
    }
}
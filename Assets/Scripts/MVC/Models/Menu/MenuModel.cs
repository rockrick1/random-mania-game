using System;

public class MenuModel : IMenuModel
{
    public event Action OnBackPressed;
    
    public IMainMenuModel MainMenuModel { get; }
    public ISettingsModel SettingsModel { get; }
    public ISongMenuModel SongMenuModel { get; }
    public SongLoader SongLoaderModel { get; }
    public IMenuInputManager InputManager { get; }
    
    public MenuModel (
        IMainMenuModel mainMenuModel,
        ISettingsModel settingsModel,
        ISongMenuModel songMenuModel,
        SongLoader songLoader,
        IMenuInputManager inputManager
    )
    {
        MainMenuModel = mainMenuModel;
        SettingsModel = settingsModel;
        SongMenuModel = songMenuModel;
        SongLoaderModel = songLoader;
        InputManager = inputManager;
    }

    public void Initialize ()
    {
        AddListeners();
        // SongLoaderModel.Initialize();
        MainMenuModel.Initialize();
        SettingsModel.Initialize();
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
        SettingsModel.Dispose();
        SongMenuModel.Dispose();
    }
}
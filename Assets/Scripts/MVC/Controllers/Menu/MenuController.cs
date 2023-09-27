using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class MenuController : IDisposable
{
    readonly MenuView view;
    readonly MainMenuController mainMenuController;
    readonly SongMenuController songMenuController;
    readonly SettingsController settingsController;
    readonly ISongMenuModel songMenuModel;
    readonly IMenuModel model;

    MenuType currentMenu = MenuType.MainMenu;
    
    public MenuController (
        MenuView view,
        MainMenuController mainMenuController,
        SettingsController settingsController,
        SongMenuController songMenuController,
        ISongMenuModel songMenuModel,
        IMenuModel model
    )
    {
        this.view = view;
        this.mainMenuController = mainMenuController;
        this.songMenuController = songMenuController;
        this.settingsController = settingsController;
        this.songMenuModel = songMenuModel;
        this.model = model;
    }

    public void Initialize ()
    {
        mainMenuController.Initialize();
        songMenuController.Initialize();
        settingsController.Initialize();
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnBackPressed += HandleBackPressed;
        mainMenuController.OnOpenSongMenu += HandleOpenSongMenu;
        mainMenuController.OnOpenEditor += HandleOpenEditor;
        mainMenuController.OnOpenSettings += HandleOpenSettings;
        songMenuModel.OnSongSelected += HandleSongSelected;
        songMenuController.OnBackPressed += HandleBackPressed;
    }

    void RemoveListeners ()
    {
        model.OnBackPressed -= HandleBackPressed;
        mainMenuController.OnOpenSongMenu -= HandleOpenSongMenu;
        mainMenuController.OnOpenEditor -= HandleOpenEditor;
        mainMenuController.OnOpenSettings -= HandleOpenSettings;
        songMenuModel.OnSongSelected -= HandleSongSelected;
        songMenuController.OnBackPressed -= HandleBackPressed;
    }

    void HandleSongSelected (string songId, string songDifficultyName)
    {
        GameContext.Current.SelectedSongId = songId;
        GameContext.Current.SelectedSongDifficulty = songDifficultyName;
        SceneManager.LoadScene("Game");
    }

    void HandleBackPressed () => CloseMenu(currentMenu);

    void HandleOpenSongMenu () => OpenMenu(MenuType.SongMenu);

    void HandleOpenSettings () => OpenMenu(MenuType.Settings);
    
    void HandleOpenEditor () => SceneManager.LoadScene("SongEditor");

    void OpenMenu (MenuType menu)
    {
        switch (menu)
        {
            case MenuType.Settings:
                settingsController.Open();
                currentMenu = MenuType.Settings;
                break;
            case MenuType.SongMenu:
                songMenuController.Open();
                mainMenuController.ZoomIn();
                view.AnimateParticlesScale(5f);
                currentMenu = MenuType.SongMenu;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void CloseMenu (MenuType menu)
    {
        switch (menu)
        {
            case MenuType.MainMenu:
                Application.Quit();
                break;
            case MenuType.Settings:
                settingsController.Close();
                currentMenu = MenuType.MainMenu;
                break;
            case MenuType.SongMenu:
                songMenuController.Close();
                mainMenuController.ZoomOut();
                view.AnimateParticlesScale(1f);
                currentMenu = MenuType.MainMenu;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}
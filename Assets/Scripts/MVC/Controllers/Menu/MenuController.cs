﻿using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class MenuController : IDisposable
{
    readonly MenuView view;
    readonly MainMenuController mainMenuController;
    readonly SongMenuController songMenuController;
    readonly SettingsController settingsController;
    readonly IMenuModel model;

    MenuType currentMenu = MenuType.MainMenu;
    
    public MenuController (
        MenuView view,
        MainMenuController mainMenuController,
        SongMenuController songMenuController,
        SettingsController settingsController,
        IMenuModel model
    )
    {
        this.view = view;
        this.mainMenuController = mainMenuController;
        this.songMenuController = songMenuController;
        this.settingsController = settingsController;
        this.model = model;
    }

    public void Initialize ()
    {
        mainMenuController.Initialize();
        songMenuController.Initialize();
        songMenuController.Close();
        settingsController.Initialize();
        settingsController.Close();
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnBackPressed += HandleBackPressed;
        mainMenuController.OnOpenSongMenu += HandleOpenSongMenu;
        mainMenuController.OnOpenEditor += HandleOpenEditor;
        mainMenuController.OnOpenSettings += HandleOpenSettings;
        songMenuController.OnSongSelected += HandleSongSelected;
        songMenuController.OnBackPressed += HandleBackPressed;
    }

    void RemoveListeners ()
    {
        model.OnBackPressed -= HandleBackPressed;
        mainMenuController.OnOpenSongMenu -= HandleOpenSongMenu;
        mainMenuController.OnOpenEditor -= HandleOpenEditor;
        mainMenuController.OnOpenSettings -= HandleOpenSettings;
        songMenuController.OnSongSelected -= HandleSongSelected;
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
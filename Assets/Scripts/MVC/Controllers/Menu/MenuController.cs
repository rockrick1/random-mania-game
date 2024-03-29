﻿using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class MenuController : IDisposable
{
    readonly MenuView view;
    readonly MainMenuController mainMenuController;
    readonly SongMenuController songMenuController;
    readonly SettingsController settingsController;
    readonly MenuMusicController menuMusicController;
    readonly ISongMenuModel songMenuModel;
    readonly IMenuModel model;

    MenuType currentMenu = MenuType.MainMenu;
    
    public MenuController (
        MenuView view,
        MainMenuController mainMenuController,
        SettingsController settingsController,
        SongMenuController songMenuController,
        MenuMusicController menuMusicController,
        ISongMenuModel songMenuModel,
        IMenuModel model
    )
    {
        this.view = view;
        this.mainMenuController = mainMenuController;
        this.songMenuController = songMenuController;
        this.settingsController = settingsController;
        this.menuMusicController = menuMusicController;
        this.songMenuModel = songMenuModel;
        this.model = model;
    }

    public void Initialize ()
    {
        mainMenuController.Initialize();
        songMenuController.Initialize();
        settingsController.Initialize();
        menuMusicController.Initialize();
        AddListeners();
    }

    void AddListeners ()
    {
        model.OnBackPressed += HandleBackPressed;
        mainMenuController.OnOpenSongMenu += HandleOpenSongMenu;
        mainMenuController.OnOpenEditor += HandleOpenEditor;
        mainMenuController.OnOpenSettings += HandleOpenSettings;
        mainMenuController.OnQuit += HandleBackPressed;
        songMenuController.OnBackPressed += HandleBackPressed;
        settingsController.OnClose += HandleBackPressed;
    }

    void RemoveListeners ()
    {
        model.OnBackPressed -= HandleBackPressed;
        mainMenuController.OnOpenSongMenu -= HandleOpenSongMenu;
        mainMenuController.OnOpenEditor -= HandleOpenEditor;
        mainMenuController.OnOpenSettings -= HandleOpenSettings;
        mainMenuController.OnQuit -= HandleBackPressed;
        songMenuController.OnBackPressed -= HandleBackPressed;
        settingsController.OnClose -= HandleBackPressed;
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
                mainMenuController.Close();
                currentMenu = MenuType.Settings;
                break;
            case MenuType.SongMenu:
                songMenuController.Open();
                mainMenuController.Close();
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
                mainMenuController.Open();
                currentMenu = MenuType.MainMenu;
                break;
            case MenuType.SongMenu:
                songMenuController.Close();
                mainMenuController.Open();
                currentMenu = MenuType.MainMenu;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Dispose ()
    {
        mainMenuController.Dispose();
        songMenuController.Dispose();
        settingsController.Dispose();
        menuMusicController.Dispose();
        RemoveListeners();
    }
}
public static class MenuModelFactory
{
    public static MenuModel Create (IMenuInputManager inputManager, IAudioManager audioManager)
    {
        SongLoader songLoader = SongLoader.Instance;
        IMainMenuModel mainMenuModel = new MainMenuModel();
        ISettingsModel settingsModel = new SettingsModel(audioManager);
        ISongMenuModel songMenuModel = new SongMenuModel(songLoader);
        var model = new MenuModel(mainMenuModel, settingsModel, songMenuModel, songLoader, inputManager, audioManager);
        return model;
    }
}
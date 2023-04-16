public static class MenuModelFactory
{
    public static MenuModel Create (IMenuInputManager inputManager, IAudioManager audioManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        IMainMenuModel mainMenuModel = new MainMenuModel();
        ISettingsModel settingsModel = new SettingsModel(audioManager);
        ISongMenuModel songMenuModel = new SongMenuModel(songLoaderModel);
        var model = new MenuModel(mainMenuModel, settingsModel, songMenuModel, songLoaderModel, inputManager);
        return model;
    }
}
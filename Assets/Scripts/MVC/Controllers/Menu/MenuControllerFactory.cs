public static class MenuControllerFactory
{
    public static MenuController Create (MenuView view, MenuModel model)
    {
        MainMenuController mainMenuController = new(view.MainMenuView, model.MainMenuModel);
        SongMenuController songMenuController = new(view.SongMenuView, model.SongMenuModel);
        SettingsController settingsController = new(view.SettingsView, model.SettingsModel);
        MenuMusicController menuMusicController = new MenuMusicController(model.AudioManager);
        return new MenuController(
            view,
            mainMenuController,
            settingsController,
            songMenuController,
            menuMusicController,
            model.SongMenuModel,
            model
        );
    }
}
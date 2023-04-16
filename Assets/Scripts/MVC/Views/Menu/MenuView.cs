using UnityEngine;

public class MenuView : MonoBehaviour
{
    [SerializeField] MainMenuView mainMenuView;
    [SerializeField] SettingsView settingsView;
    [SerializeField] SongMenuView songMenuView;
    
    public MainMenuView MainMenuView => mainMenuView;
    public SettingsView SettingsView => settingsView;
    public SongMenuView SongMenuView => songMenuView;
    
    
}
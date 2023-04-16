using UnityEngine;

public class MenuView : MonoBehaviour
{
    [SerializeField] MainMenuView mainMenuView;
    [SerializeField] SongMenuView songMenuView;
    [SerializeField] SettingsView settingsView;
    
    public MainMenuView MainMenuView => mainMenuView;
    public SongMenuView SongMenuView => songMenuView;
    public SettingsView SettingsView => settingsView;
    
    
}
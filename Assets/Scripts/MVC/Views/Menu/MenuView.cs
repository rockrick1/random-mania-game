using DG.Tweening;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    [SerializeField] MainMenuView mainMenuView;
    [SerializeField] SongMenuView songMenuView;
    [SerializeField] SettingsView settingsView;

    [SerializeField] Transform backgroundParticles;
    
    public MainMenuView MainMenuView => mainMenuView;
    public SongMenuView SongMenuView => songMenuView;
    public SettingsView SettingsView => settingsView;

    public void AnimateParticlesScale(float scale) =>
        backgroundParticles.DOScale(new Vector3(scale, scale, 1), 1f).SetEase(Ease.InOutCubic);
}
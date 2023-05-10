using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameView view;
    [SerializeField] GameInputManager inputManager;
    [SerializeField] AudioManager audioManager;

    public static bool IsPaused = false;

    public GameModel Model { get; private set; }
    public GameController Controller { get; private set; }
    public GameView View => view;

    void Start ()
    {
        Application.targetFrameRate = 60;
        AudioListener.volume = 0.2f;
        Model = GameModelFactory.Create(inputManager, audioManager);
        Controller = GameControllerFactory.Create(View, Model);
        Initialize();
    }

    void Initialize ()
    {
        audioManager.Initialize();
        Model.Initialize();
        Controller.Initialize();
    }

    void OnDestroy ()
    {
        Model.Dispose();
        Controller.Dispose();
    }
}
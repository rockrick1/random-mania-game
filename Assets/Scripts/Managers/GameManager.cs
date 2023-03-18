using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameView view;

    public GameModel Model { get; private set; }
    public GameController Controller { get; private set; }
    public GameView View => view;
    
    void Start()
    {
        Model = GameModelFactory.Create();
        Controller = GameControllerFactory.Create(View, Model);
        Initialize();
    }

    void Initialize()
    {
        Model.Initialize();
        Controller.Initialize();
    }

    void OnDestroy()
    {
        Model.Dispose();
        Controller.Dispose();
    }
}
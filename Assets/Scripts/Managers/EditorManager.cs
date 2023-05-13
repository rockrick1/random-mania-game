using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] EditorView view;
    [SerializeField] EditorInputManager inputManager;

    public EditorModel Model { get; private set; }
    public EditorController Controller { get; private set; }
    public EditorView View => view;

    AudioManager audioManager;
    
    void Start ()
    {
        Application.targetFrameRate = 60;
        audioManager = AudioManager.GetOrCreate();
        Model = EditorModelFactory.Create(inputManager, audioManager);
        Controller = EditorControllerFactory.Create(View, Model);
        Initialize();
    }

    void Initialize ()
    {
        Model.Initialize();
        Controller.Initialize();
    }

    void OnDestroy ()
    {
        Model.Dispose();
        Controller.Dispose();
    }
}
using UnityEngine;

public class SongEditorManager : MonoBehaviour
{
    [SerializeField] EditorView view;
    [SerializeField] EditorInputManager inputManager;

    public SongEditorModel Model { get; private set; }
    public EditorController Controller { get; private set; }
    public EditorView View => view;

    void Start ()
    {
        Application.targetFrameRate = 60;
        AudioListener.volume = 0.2f;
        Model = SongEditorModelFactory.Create(inputManager);
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
using System;
using UnityEngine;

public class EditorInputManager : MonoBehaviour, IEditorInputManager
{
    public event Action OnSongPlayPause;
    public event Action<float> OnSongScroll;

    void Start ()
    {
    }

    void Update ()
    {
        EditorInputCheck();
    }

    void EditorInputCheck ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSongPlayPause?.Invoke();
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            OnSongScroll?.Invoke(Input.mouseScrollDelta.y);
        }
    }
}
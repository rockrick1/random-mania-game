using System;
using UnityEngine;

public class EditorInputManager : MonoBehaviour, IEditorInputManager
{
    public event Action OnSongPlayPause;
    public event Action<float> OnSongScroll;
    public event Action<float> OnZoomScroll;

    void Start ()
    {
    }

    public Vector3 GetMousePos () => Input.mousePosition;

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
            if (Input.GetKey(KeyCode.LeftShift))
                OnZoomScroll?.Invoke(Input.mouseScrollDelta.y);
            else
                OnSongScroll?.Invoke(Input.mouseScrollDelta.y);
        }
    }
}
using System;
using TMPro;
using UnityEngine;

public class NewSongView : MonoBehaviour
{
    public event Action<string> OnCreateSong;
    
    [SerializeField] TMP_InputField songNameInput;
    [SerializeField] UIClickHandler create;
    [SerializeField] UIClickHandler cancel;

    void Start ()
    {
        create.OnLeftClick.AddListener(HandleCreate);
        cancel.OnLeftClick.AddListener(HandleCancel);
        Close();
    }

    void HandleCreate () => OnCreateSong?.Invoke(songNameInput.text);

    void HandleCancel ()
    {
        songNameInput.text = string.Empty;
        Close();
    }

    public void Open () => gameObject.SetActive(true);

    public void Close () => gameObject.SetActive(false);
}

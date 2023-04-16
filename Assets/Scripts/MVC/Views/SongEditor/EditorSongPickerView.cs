using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorSongPickerView : MonoBehaviour
{
    public event Action<string> OnSongPicked;
    public event Action OnOpenFolderClicked;
    
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] UIClickHandler openFolderButton;

    public void LoadOptions (IEnumerable<string> options)
    {
        dropdown.AddOptions((List<string>) options);
        openFolderButton.OnLeftClick.AddListener(() => OnOpenFolderClicked?.Invoke());
    }

    public void HandleInputChanged ()
    {
        OnSongPicked?.Invoke(dropdown.options[dropdown.value].text);
    }
}
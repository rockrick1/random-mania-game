using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorSongPickerView : MonoBehaviour
{
    public event Action<string> OnSongPicked;
    
    [SerializeField] TMP_Dropdown dropdown;

    public void LoadOptions (IEnumerable<string> options)
    {
        dropdown.AddOptions((List<string>) options);
    }

    public void HandleInputChanged ()
    {
        OnSongPicked?.Invoke(dropdown.options[dropdown.value].text);
    }
}
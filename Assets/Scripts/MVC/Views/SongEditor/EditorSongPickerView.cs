using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorSongPickerView : MonoBehaviour
{
    public event Action<string, string> OnSongPicked;
    public event Action OnOpenFolderClicked;
    public event Action OnNewSongClicked;
    public event Action OnRefreshClicked;
    
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] UIClickHandler openFolderButton;
    [SerializeField] UIClickHandler newSongButton;
    [SerializeField] UIClickHandler refreshButton;

    public void LoadOptions (IEnumerable<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(){""});
        dropdown.AddOptions((List<string>) options);
        openFolderButton.OnLeftClick.AddListener(() => OnOpenFolderClicked?.Invoke());
        newSongButton.OnLeftClick.AddListener(() => OnNewSongClicked?.Invoke());
        refreshButton.OnLeftClick.AddListener(() => OnRefreshClicked?.Invoke());
    }

    public void PickSong (string songId)
    {
        dropdown.value = dropdown.options.FindIndex(x => x.text == songId);
    }

    public void HandleInputChanged ()
    {
        OnSongPicked?.Invoke(dropdown.options[dropdown.value].text, "SOCORRO");
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongMenuView : MonoBehaviour
{
    public event Action<string> OnSongClicked;
    public event Action OnBackPressed;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] VerticalLayoutGroup songsList;
    [SerializeField] SongEntryView songEntryPrefab;

    [SerializeField] UIClickHandler backButton;

    void Start ()
    {
        backButton.OnLeftClick.AddListener(() => OnBackPressed?.Invoke());
    }

    public void Setup (IReadOnlyList<ISongSettings> songs)
    {
        foreach (ISongSettings song in songs)
        {
            SongEntryView entry = Instantiate(songEntryPrefab, songsList.transform);
            entry.transform.Rotate(Vector3.forward, -scrollRect.transform.rotation.z);
            entry.Setup(song);
            entry.Button.OnLeftClick.AddListener(() => OnSongClicked?.Invoke(song.Id));
        }
    }
    
    public void Open ()
    {
        gameObject.SetActive(true);
    }

    public void Close ()
    {
        gameObject.SetActive(false);
    }
}
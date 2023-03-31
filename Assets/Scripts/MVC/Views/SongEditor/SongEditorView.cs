using System.Collections.Generic;
using UnityEngine;

public class SongEditorView : MonoBehaviour
{
    [SerializeField] AudioSource songPlayer;
    [SerializeField] WaveForm2D waveForm2D;
    [SerializeField] EditorSongPickerView editorSongPickerView;
    [SerializeField] EditorSongDetailsView editorSongDetailsView;
    [SerializeField] Transform horizontalSeparatorsParent;
    [SerializeField] RectTransform horizontalSeparatorPrefab;

    List<RectTransform> horizontalSeparators = new();

    public WaveForm2D WaveForm2D => waveForm2D;
    public EditorSongPickerView EditorSongPickerView => editorSongPickerView;
    public EditorSongDetailsView EditorSongDetailsView => editorSongDetailsView;

    public void SetSong (AudioClip clip) => songPlayer.clip = clip;

    public void PlayPauseSong ()
    {
        if (songPlayer.isPlaying)
            songPlayer.Pause();
        else
            songPlayer.Play();
    }

    public void ChangeSongTime (float amount) => songPlayer.time += amount;

    public void AddHorizontalSeparator (float distance)
    {
        RectTransform instance = Instantiate(horizontalSeparatorPrefab, horizontalSeparatorsParent);
        instance.sizeDelta = new Vector2(instance.sizeDelta.x, distance);
        horizontalSeparators.Add(instance);
    }

    public void SetHorizontalSeparatorsDistance (float distance)
    {
        foreach (RectTransform child in horizontalSeparators)
        {
            child.sizeDelta = new Vector2(child.sizeDelta.x, distance);
        }
    }

    public void ClearSeparators ()
    {
        foreach (RectTransform child in horizontalSeparators)
        {
            Destroy(child);
        }
        horizontalSeparators.Clear();
    }
}
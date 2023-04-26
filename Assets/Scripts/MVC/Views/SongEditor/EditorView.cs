using System.Collections.Generic;
using UnityEngine;

public class EditorView : MonoBehaviour
{
    [SerializeField] AudioSource songPlayer;
    [SerializeField] WaveForm2D waveForm2D;
    [SerializeField] EditorSongPickerView editorSongPickerView;
    [SerializeField] EditorNewSongView editorNewSongView;
    [SerializeField] EditorSongDetailsView editorSongDetailsView;
    [SerializeField] EditorTopBarView editorTopBarView;
    [SerializeField] EditorSongView editorSongView;
    [SerializeField] Transform horizontalSeparatorsParent;
    [SerializeField] RectTransform horizontalSeparatorPrefab;
    [SerializeField] UIClickHandler backButton;

    public AudioSource SongPlayer => songPlayer;
    public WaveForm2D WaveForm2D => waveForm2D;
    public EditorSongPickerView EditorSongPickerView => editorSongPickerView;
    public EditorNewSongView EditorNewSongView => editorNewSongView;
    public EditorSongDetailsView EditorSongDetailsView => editorSongDetailsView;
    public EditorTopBarView EditorTopBarView => editorTopBarView;
    public EditorSongView EditorSongView => editorSongView;
    public UIClickHandler BackButton => backButton;

    List<RectTransform> horizontalSeparators = new();

    public void SetSong (AudioClip clip) => songPlayer.clip = clip;

    public void PlayPauseSong ()
    {
        if (songPlayer.isPlaying)
            songPlayer.Pause();
        else
            songPlayer.Play();
    }

    public void SetSongTime (float time) =>
        songPlayer.time = Mathf.Clamp(time, 0, songPlayer.clip.length - .1f);

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
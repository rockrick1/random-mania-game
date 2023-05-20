using UnityEngine;

public class EditorView : MonoBehaviour
{
    [SerializeField] WaveForm2D waveForm2D;
    
    [SerializeField] EditorSongPickerView editorSongPickerView;
    [SerializeField] EditorNewSongView editorNewSongView;
    [SerializeField] EditorSongDetailsView editorSongDetailsView;
    [SerializeField] EditorTopBarView editorTopBarView;
    [SerializeField] EditorSongView editorSongView;
    
    [SerializeField] UIClickHandler backButton;

    public WaveForm2D WaveForm2D => waveForm2D;
    public EditorSongPickerView EditorSongPickerView => editorSongPickerView;
    public EditorNewSongView EditorNewSongView => editorNewSongView;
    public EditorSongDetailsView EditorSongDetailsView => editorSongDetailsView;
    public EditorTopBarView EditorTopBarView => editorTopBarView;
    public EditorSongView EditorSongView => editorSongView;
    public UIClickHandler BackButton => backButton;
}
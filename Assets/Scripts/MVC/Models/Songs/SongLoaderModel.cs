using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class SongLoaderModel : ISongLoaderModel
{
    const string BPM_KEY = "bpm";
    const string APPROACH_RATE_KEY = "approachRate";
    const string DIFFICULTY_KEY = "difficulty";
    const string STARTING_TIME_EY = "startingTime";
    const string NOTES_KEY = "notes";

    public event Action OnSongLoaded;
    public event Action OnSongSaved;

    public SongSettings Settings { get; private set; }
    public AudioClip Audio { get; private set; }

    public void Initialize ()
    {
    }

    public void LoadSong (string songId)
    {
        Audio = Resources.Load<AudioClip>($"Songs/{songId}/song");
        TextAsset songAsset = Resources.Load<TextAsset>($"Songs/{songId}/song");
        if (songAsset == null)
        {
            Debug.LogException(new ArgumentException($"Song {songId} not found!"));
            return;
        }
        LoadSongTextData(songAsset.text);
        Settings.Id = songId;
        OnSongLoaded?.Invoke();
    }

    public void SaveSong (ISongSettings settings)
    {
        string path = $"Songs/{settings.Id}/song";
        TextAsset songAsset = Resources.Load<TextAsset>(path);
        if (songAsset == null)
        {
            Debug.LogException(new ArgumentException($"Song {settings.Id} not found while saving!"));
            return;
        }
        SaveSongTextData(songAsset, settings);
        OnSongSaved?.Invoke();
    }

    void LoadSongTextData (string file)
    {
        Settings = new SongSettings();
        string[] lines = file.Split('\n');

        string key = "";
        foreach (string t in lines)
        {
            string line = Regex.Replace(t, @"\s", string.Empty);
            if (string.IsNullOrEmpty(line))
                continue;

            if (line.Contains('['))
            {
                key = Regex.Replace(line, @"\[|\]", string.Empty);
                continue;
            }

            switch (key)
            {
                case BPM_KEY:
                    Settings.Bpm = ParseFloat(line);
                    break;
                case APPROACH_RATE_KEY:
                    Settings.ApproachRate = ParseFloat(line);
                    break;
                case DIFFICULTY_KEY:
                    Settings.Difficulty = ParseFloat(line);
                    break;
                case STARTING_TIME_EY:
                    Settings.StartingTime = ParseFloat(line);
                    break;
                case NOTES_KEY:
                    string[] values = line.Split(',');
                    Settings.Notes.Add(new Note(ParseFloat(values[0]), int.Parse(values[1])));
                    break;
                default:
                    throw new Exception($"Unknown key found while parsing song: {key}");
            }
        }
    }
    
    void SaveSongTextData (TextAsset songAsset, ISongSettings settings)
    {
        string text = string.Empty;

        text += $"[{BPM_KEY}]\n{settings.Bpm.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{DIFFICULTY_KEY}]\n{settings.Difficulty.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{STARTING_TIME_EY}]\n{settings.StartingTime.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{APPROACH_RATE_KEY}]\n{settings.ApproachRate.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{NOTES_KEY}]\n";

        foreach (Note note in settings.Notes)
            text += $"{note.Timestamp.ToString(CultureInfo.InvariantCulture)},{note.Position}\n";

        File.WriteAllText(AssetDatabase.GetAssetPath(songAsset), text);
        EditorUtility.SetDirty(songAsset);
    }

    double ParseDouble (string s) => double.Parse(s, CultureInfo.InvariantCulture);
    float ParseFloat (string s) => float.Parse(s, CultureInfo.InvariantCulture);
}
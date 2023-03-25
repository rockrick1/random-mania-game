using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class SongLoaderModel : ISongLoaderModel
{
    const string BPM_KEY = "bpm";
    const string APPROACH_RATE_KEY = "approachRate";
    const string DIFFICULTY_KEY = "difficulty";
    const string NOTES_KEY = "notes";
    
    public SongSettings Settings { get; private set; }
    public AudioClip Audio { get; private set; }

    public void Initialize ()
    {
    }

    public void LoadSong (string songId)
    {
        Audio = Resources.Load<AudioClip>($"Songs/{songId}/song");
        TextAsset songAsset = Resources.Load<TextAsset>($"Songs/{songId}/song");
        LoadSongTextData(songAsset.text);
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
                    Settings.Bpm = ParseDouble(line);
                    break;
                case APPROACH_RATE_KEY:
                    Settings.ApproachRate = ParseFloat(line);
                    break;
                case DIFFICULTY_KEY:
                    Settings.Difficulty = ParseFloat(line);
                    break;
                case NOTES_KEY:
                    string[] values = line.Split(',');
                    Settings.Notes.Add(new Note(ParseDouble(values[0]), int.Parse(values[1])));
                    break;
                default:
                    throw new Exception($"Unknown key found while parsing song: {key}");
            }
        }
    }

    double ParseDouble (string s) => double.Parse(s.Replace('.', ','));
    float ParseFloat (string s) => float.Parse(s.Replace('.', ','));
}
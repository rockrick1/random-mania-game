using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class SongLoaderModel : ISongLoaderModel
{
    const string AUDIO_EXTENSION = ".mp3";
    const string BPM_KEY = "bpm";
    const string APPROACH_RATE_KEY = "approachRate";
    const string DIFFICULTY_KEY = "difficulty";
    const string STARTING_TIME_EY = "startingTime";
    const string NOTES_KEY = "notes";
    
    static readonly string songResourcesPath = "Songs";

    public event Action OnSongLoaded;
    public event Action OnSongSaved;
    public event Action OnSongCreated;

    public SongSettings Settings { get; private set; }
    public AudioClip Audio { get; private set; }
    public string SongsPath => Path.Combine(Application.persistentDataPath, "SongsDatabase");

    string textPath;
    string audioPath;

    public void Initialize ()
    {
        TryCreateDefaultFiles();
    }

    void TryCreateDefaultFiles ()
    {
        if (!Directory.Exists(SongsPath))
            Directory.CreateDirectory(SongsPath);
        TextAsset[] songAssets = Resources.LoadAll<TextAsset>(songResourcesPath);
    
        foreach (TextAsset songAsset in songAssets)
        {
            string dirPath = Path.Combine(SongsPath, songAsset.name);
            string filePath = Path.Combine(dirPath, "song.txt");
            
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            if (File.Exists(filePath))
                continue;
            File.WriteAllBytes(filePath, songAsset.bytes);
        }
    }

    public void CreateSongFolder (string songId)
    {
        if (!Directory.Exists(SongsPath))
            throw new Exception("Songs path doest not exist! something went wrong on initialization, it seems.");
        string dirPath = Path.Combine(SongsPath, songId);
        Directory.CreateDirectory(dirPath);
        OnSongCreated?.Invoke();
    }

    public void LoadSong (string songId)
    {
        bool isDefault = false;
        Settings = new SongSettings { Id = songId };
        AudioClip clip = Resources.Load<AudioClip>(GetResourcePath(songId));
        if (clip != null)
        {
            isDefault = true;
            Audio = clip;
        }
        CoroutineRunner.Instance.StartCoroutine(nameof(LoadSong), LoadSong(isDefault));
    }

    IEnumerator LoadSong (bool isDefaultSong)
    {
        textPath = GetTextPath(Settings.Id);
        audioPath = GetAudioPath(Settings.Id);

        string songText = string.Empty;
        if (File.Exists(textPath))
            songText = File.ReadAllText(textPath);
        else
            File.WriteAllText(textPath, songText);

        if (!isDefaultSong)
            yield return ReadAudioFile(audioPath);
        if (Audio == null)
        {
            Debug.LogException(new ArgumentException($"Could not load song {Settings.Id}!"));
            yield break;
        }
        
        LoadSongSettings(songText);
        OnSongLoaded?.Invoke();
    }

    public void SaveSong (ISongSettings settings)
    {
        SaveSongSettings(settings);
        OnSongSaved?.Invoke();
    }

    public IReadOnlyList<string> GetAllSongDirs ()
    {
        List<string> ret = new();
        string[] dirs = Directory.GetDirectories(SongsPath);
        foreach (string dir in dirs)
            ret.Add(dir.Split('\\').Last());
        return ret;
    }

    public IReadOnlyList<ISongSettings> GetAllSongSettings ()
    {
        List<ISongSettings> ret = new();
        foreach (string dir in GetAllSongDirs())
        {
            textPath = GetTextPath(dir);
            if (!File.Exists(textPath))
                continue;
            
            Settings = new SongSettings {Id = dir};

            string songText = File.ReadAllText(textPath);
            LoadSongSettings(songText);
            ret.Add(Settings.Clone());
        }

        return ret;
    }

    public bool SongExists (string songId) => Directory.Exists(Path.Combine(SongsPath, songId));

    void LoadSongSettings (string file)
    {
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
    
    void SaveSongSettings (ISongSettings settings)
    {
        string text = string.Empty;

        text += $"[{BPM_KEY}]\n{settings.Bpm.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{DIFFICULTY_KEY}]\n{settings.Difficulty.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{STARTING_TIME_EY}]\n{settings.StartingTime.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{APPROACH_RATE_KEY}]\n{settings.ApproachRate.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{NOTES_KEY}]\n";

        foreach (Note note in settings.Notes)
            text += $"{note.Timestamp.ToString(CultureInfo.InvariantCulture)},{note.Position}\n";

        File.WriteAllText(textPath, text);
    }
    
    IEnumerator ReadAudioFile (string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new Exception(
                $"audio file was not found on driectory {path}. Make sure there is a {AUDIO_EXTENSION} file in the song directory.");
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, AudioType.MPEG);
        yield return req.SendWebRequest();
        Audio = DownloadHandlerAudioClip.GetContent(req);
    }
    
    string GetTextPath(string songId) => Path.Combine(SongsPath, songId, "song.txt");
    
    string GetAudioPath(string songId)
    {
        string songPath = Path.Combine(SongsPath, songId);
        foreach (string file in Directory.GetFiles(songPath))
        {
            if (file.EndsWith(AUDIO_EXTENSION))
                return file;
        }
        return string.Empty;
    }

    string GetResourcePath(string songId) => Path.Combine(songResourcesPath, songId);

    double ParseDouble (string s) => double.Parse(s, CultureInfo.InvariantCulture);
    float ParseFloat (string s) => float.Parse(s, CultureInfo.InvariantCulture);
}
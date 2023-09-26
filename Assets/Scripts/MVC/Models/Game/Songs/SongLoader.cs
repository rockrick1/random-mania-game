using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class SongLoader : MonoBehaviour
{
    const string AUDIO_EXTENSION = ".mp3";
    const string SONG_RESOURCES_PATH = "Songs";
    
    const string TITLE_KEY = "title";
    const string ARTIST_KEY = "artist";
    const string DIFFICULTY_NAME_KEY = "difficultyName";
    const string BPM_KEY = "bpm";
    const string APPROACH_RATE_KEY = "approachRate";
    const string DIFFICULTY_KEY = "difficulty";
    const string STARTING_TIME_EY = "startingTime";
    const string NOTES_KEY = "notes";

    public event Action OnSongSaved;
    public event Action OnSongCreated;

    public static SongLoader Instance
    {
        get
        {
            if (instance == null)
            {
                SongLoader songLoader = Instantiate(Resources.Load<SongLoader>("SongLoader"));
                DontDestroyOnLoad(songLoader.gameObject);
                instance = songLoader;
            }
            return instance;
        }
    }

    public Dictionary<string, Dictionary<string, ISongSettings>> SongsCache { get; } = new();
    public Dictionary<string, AudioClip> SongsAudioCache { get; } = new();
    public string SongsPath => Path.Combine(Application.persistentDataPath, "SongsDatabase");
    public string SelectedSongId { get; set; }
    public string SelectedSongDifficulty { get; set; }

    static SongLoader instance;

    public void Awake ()
    {
        TryCreateDefaultFiles();
        LoadAllSongSettings();
        LoadAllSongAudios();
    }

    public static string GetSongId (string songName, string artistName) => $"{artistName} - {songName}";

    public ISongSettings GetSongSettings (string songId, string difficultyName)
    {
        if (!SongsCache.TryGetValue(songId, out Dictionary<string, ISongSettings> difficultiesDict))
            throw new IndexOutOfRangeException($"No song with id '{songId}' found");
        if (!difficultiesDict.TryGetValue(difficultyName, out ISongSettings settings))
            throw new IndexOutOfRangeException($"Song '{songId}' doesnt't have a difficulty called '{difficultyName}'");
        return settings;
    }

    public ISongSettings GetSelectedSongSettings () => GetSongSettings(SelectedSongId, SelectedSongDifficulty);

    public AudioClip GetSelectedSongAudio ()
    {
        if (!SongsAudioCache.TryGetValue(SelectedSongId, out AudioClip clip))
            throw new IndexOutOfRangeException($"No song with id '{SelectedSongId}' found");
        return clip;
    }

    public void CreateSong (string songName, string artistName, string songDifficultyName)
    {
        if (!Directory.Exists(SongsPath))
            throw new Exception("Songs path doest not exist! something went wrong on initialization, it seems.");
        string dirPath = Path.Combine(SongsPath, GetSongId(songName, artistName));
        Directory.CreateDirectory(dirPath);
        SongSettings newSongSettings = new()
        {
            Id = GetSongId(songName, artistName),
            Title = songName,
            Artist = artistName,
            DifficultyName = songDifficultyName
        };
        SaveSongTextFile(newSongSettings);
        OnSongCreated?.Invoke();
    }

    public void SaveSong (ISongSettings settings)
    {
        SaveSongTextFile(settings);
        OnSongSaved?.Invoke();
    }

    public bool SongExists (string songName, string artistName) =>
        Directory.Exists(Path.Combine(SongsPath, GetSongId(songName, artistName)));

    public IReadOnlyList<ISongSettings> GetAllSongSettings ()
    {
        List<ISongSettings> ret = new();
        foreach (Dictionary<string, ISongSettings> songGroup in SongsCache.Values)
            ret.AddRange(songGroup.Values);
        return ret;
    }

    void TryCreateDefaultFiles ()
    {
        if (!Directory.Exists(SongsPath))
            Directory.CreateDirectory(SongsPath);
        TextAsset[] songAssets = Resources.LoadAll<TextAsset>(SONG_RESOURCES_PATH);
    
        foreach (TextAsset songAsset in songAssets)
        {
            SongSettings settings = ReadSongTextFile(songAsset.text);
            string dirPath = Path.Combine(SongsPath, settings.Id);
            string filePath = Path.Combine(dirPath, "song.txt");
            
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            if (File.Exists(filePath))
                continue;
            File.WriteAllBytes(filePath, songAsset.bytes);
        }
    }

    void LoadAllSongSettings ()
    {
        string[] temp = Directory.GetDirectories(SongsPath);
        List<string> dirs = temp.Select(dir => dir.Split('\\').Last()).ToList();
        
        foreach (string dir in dirs)
        {
            string textPath = GetTextPath(dir);
            if (!File.Exists(textPath))
                continue;
            
            string songText = File.ReadAllText(textPath);
            ISongSettings settings = ReadSongTextFile(songText);
            if (!SongsCache.TryGetValue(settings.Id, out Dictionary<string, ISongSettings> _))
                SongsCache[settings.Id] = new Dictionary<string, ISongSettings>();
            SongsCache[settings.Id][settings.DifficultyName] = settings;
        }
    }

    void LoadAllSongAudios ()
    {
        StartCoroutine(LoadAllSongAudiosRoutine());
    }

    IEnumerator LoadAllSongAudiosRoutine ()
    {
        foreach (var songId in SongsCache.Keys)
            yield return LoadSongAudio(songId);
    }

    IEnumerator LoadSongAudio (string songId)
    {
        AudioClip clip = Resources.Load<AudioClip>(GetResourcePath(songId));
        if (clip != null)
        {
            SongsAudioCache[songId] = clip;
            yield break;
        }
        string path = GetAudioPath(songId);
        if (string.IsNullOrEmpty(path))
            throw new Exception($"audio file was not found on driectory {path}. Make sure there is a {AUDIO_EXTENSION} file in the song directory.");
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, AudioType.MPEG);
        yield return req.SendWebRequest();
        clip = DownloadHandlerAudioClip.GetContent(req);
        if (clip == null)
        {
            Debug.LogException(new ArgumentException($"Could not load song {songId}!"));
            yield break;
        }
        SongsAudioCache[songId] = clip;
    }

    SongSettings ReadSongTextFile (string file)
    {
        SongSettings result = new();
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
                case TITLE_KEY:
                    result.Title = line;
                    break;
                case ARTIST_KEY:
                    result.Artist = line;
                    break;
                case DIFFICULTY_NAME_KEY:
                    result.DifficultyName = line;
                    break;
                case BPM_KEY:
                    result.Bpm = ParseFloat(line);
                    break;
                case APPROACH_RATE_KEY:
                    result.ApproachRate = ParseFloat(line);
                    break;
                case DIFFICULTY_KEY:
                    result.Difficulty = ParseFloat(line);
                    break;
                case STARTING_TIME_EY:
                    result.StartingTime = ParseFloat(line);
                    break;
                case NOTES_KEY:
                    string[] values = line.Split(',');
                    string[] times = values[0].Split(':');
                    Note n;
                    if (times.Length > 1)
                        n = new Note(
                            ParseFloat(times[0]),
                            times.Length > 1 ? ParseFloat(times[1]) : 0,
                            int.Parse(values[1])
                        );
                    else
                        n = new Note(
                            ParseFloat(times[0]),
                            int.Parse(values[1])
                        );
                    result.Notes.Add(n);
                    break;
                default:
                    throw new Exception($"Unknown key found while parsing song: {key}");
            }
        }

        result.Id = GetSongId(result.Title, result.Artist);
        return result;
    }
    
    void SaveSongTextFile (ISongSettings settings)
    {
        string text = string.Empty;

        text += $"[{TITLE_KEY}]\n{settings.Title}\n\n";
        text += $"[{ARTIST_KEY}]\n{settings.Artist}\n\n";
        text += $"[{DIFFICULTY_NAME_KEY}]\n{settings.DifficultyName}\n\n";
        text += $"[{BPM_KEY}]\n{settings.Bpm.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{DIFFICULTY_KEY}]\n{settings.Difficulty.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{STARTING_TIME_EY}]\n{settings.StartingTime.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{APPROACH_RATE_KEY}]\n{settings.ApproachRate.ToString(CultureInfo.InvariantCulture)}\n\n";
        text += $"[{NOTES_KEY}]\n";

        foreach (Note note in settings.Notes)
        {
            string times = note.Time.ToString(CultureInfo.InvariantCulture);
            if (note.IsLong)
                times += $":{note.EndTime.ToString(CultureInfo.InvariantCulture)}";
            text += $"{times},{note.Position}\n";
        }

        File.WriteAllText(GetTextPath(settings.Id), text);
        if (!SongsCache.ContainsKey(settings.Id))
            SongsCache[settings.Id] = new Dictionary<string, ISongSettings>();
        SongsCache[settings.Id][settings.DifficultyName] = settings;
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

    string GetResourcePath(string songId) => Path.Combine(SONG_RESOURCES_PATH, songId);

    float ParseFloat (string s) => float.Parse(s, CultureInfo.InvariantCulture);
}
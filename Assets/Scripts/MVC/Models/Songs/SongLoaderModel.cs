using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class SongLoaderModel : ISongLoaderModel
{
    const string BPM_KEY = "bpm";
    const string APPROACH_RATE_KEY = "approachRate";
    const string DIFFICULTY_KEY = "difficulty";
    const string STARTING_TIME_EY = "startingTime";
    const string NOTES_KEY = "notes";

    static readonly string songsPath = Path.Combine(Application.persistentDataPath, "SongsDatabase");
    static readonly string songResourcesPath = "Songs";

    public event Action OnSongLoaded;
    public event Action OnSongSaved;

    public SongSettings Settings { get; private set; }
    public AudioClip Audio { get; private set; }

    string songId;
    string textPath;
    string audioPath;

    public void Initialize ()
    {
        TryCreateDefaultFiles();
    }

    void TryCreateDefaultFiles ()
    {
        if (!Directory.Exists(songsPath))
            Directory.CreateDirectory(songsPath);
        Object[] resources = Resources.LoadAll(songResourcesPath);

        foreach (Object obj in resources)
        {
            string dirPath = Path.Combine(songsPath, obj.name);
            
            if (obj is not TextAsset && obj is not AudioClip)
                throw new Exception($"object {obj.name} in {dirPath} is neither text nor audio!");

            bool isText = obj is TextAsset;
            string filePath = Path.Combine(dirPath, "song" + (isText ? ".txt" : ".mp3"));
            
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            if (File.Exists(filePath))
                continue;
            if (isText)
                File.WriteAllBytes(filePath, (obj as TextAsset).bytes);
            else
            {
                
                AudioClip clip = (AudioClip) obj;
                // byte[] bytes = new byte[data.Length];
                // Buffer.BlockCopy(data, 0, bytes, 0, clip.samples);
                

                int numSamples = clip.samples;
                int numChannels = clip.channels;
                int sampleRate = clip.frequency;

                // Calculate the multiplier for converting float samples to the desired bit depth
                float multiplier = Mathf.Pow(2, 16 - 1);

                float[] audioData = new float[numSamples * numChannels];
                clip.GetData(audioData, 0);

                short[] shortData = new short[audioData.Length];
                for (int i = 0; i < audioData.Length; i++)
                {
                    shortData[i] = (short)(audioData[i] * multiplier);
                }

                byte[] byteArray = new byte[shortData.Length * sizeof(short)];
                Buffer.BlockCopy(shortData, 0, byteArray, 0, byteArray.Length);

                File.WriteAllBytes(filePath, byteArray);
            }
        }
    }

    public void LoadSong (string songId)
    {
        this.songId = songId;
        CoroutineRunner.Instance.StartCoroutine(nameof(LoadSong), LoadSong());
    }

    IEnumerator LoadSong ()
    {
        Settings = new SongSettings { Id = songId };

        textPath = GetTextPath(songId);
        audioPath = GetAudioPath(songId);
        
        string songText = File.ReadAllText(textPath);
        yield return ReadAudioFile(audioPath);
        if (Audio == null || songText == string.Empty)
        {
            Debug.LogException(new ArgumentException($"Could not load song {songId}!"));
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
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, AudioType.MPEG);
        yield return req.SendWebRequest();
        Audio = DownloadHandlerAudioClip.GetContent(req);
    }
    
    string GetTextPath(string songId) => Path.Combine(songsPath, songId, "song.txt");
    string GetAudioPath(string songId) => Path.Combine(songsPath, songId, "song.mp3");

    double ParseDouble (string s) => double.Parse(s, CultureInfo.InvariantCulture);
    float ParseFloat (string s) => float.Parse(s, CultureInfo.InvariantCulture);
}
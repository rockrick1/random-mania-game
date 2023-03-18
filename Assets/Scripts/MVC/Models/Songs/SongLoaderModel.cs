using Newtonsoft.Json;
using UnityEngine;

public class SongLoaderModel : ISongLoaderModel
{
    public ISongSettings Settings { get; private set; }
    public AudioClip Audio { get; private set; }

    public void Initialize ()
    {
    }

    public void LoadSong (string songId)
    {
        var songAsset = Resources.Load<TextAsset>($"Songs/{songId}/song");
        Audio = Resources.Load<AudioClip>($"Songs/{songId}/song");
        Settings = JsonConvert.DeserializeObject<SongSettings>(songAsset.text);
    }
}
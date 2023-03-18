using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class SongSettings : ISongSettings
{
    [JsonProperty]
    public float Bpm { get; }
    
    [JsonProperty]
    public float ApproachRate { get; }
    
    [JsonProperty]
    public IReadOnlyList<Note> Notes { get; }

    public SongSettings(float bpm, float approachRate, IReadOnlyList<Note> notes)
    {
        Bpm = bpm;
        ApproachRate = approachRate;
        Notes = notes;
    }
}


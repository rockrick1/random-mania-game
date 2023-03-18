using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class SongSettings : ISongSettings
{
    public SongSettings (float bpm, float approachRate, IReadOnlyList<Note> notes)
    {
        Bpm = bpm;
        ApproachRate = approachRate;
        Notes = notes;
    }

    [JsonProperty] public float Bpm { get; }

    [JsonProperty] public float ApproachRate { get; }

    [JsonProperty] public IReadOnlyList<Note> Notes { get; }
}
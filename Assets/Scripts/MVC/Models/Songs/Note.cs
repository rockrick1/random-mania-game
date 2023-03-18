using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Note
{
    [JsonProperty("t")]
    public double Timestamp { get; }
        
    [JsonProperty("p")]
    public int Position { get; }

    public Note(double timestamp, int position)
    {
        Timestamp = timestamp;
        Position = position;
    }
}
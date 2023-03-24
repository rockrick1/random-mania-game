using Newtonsoft.Json;

public class Note
{
    public Note (double timestamp, int position)
    {
        Timestamp = timestamp;
        Position = position;
    }

    public double Timestamp { get; set; }
    public int Position { get; set; }
}
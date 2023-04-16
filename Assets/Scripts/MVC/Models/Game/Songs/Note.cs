public class Note
{
    public float Timestamp { get; set; }
    public int Position { get; set; }
    
    public Note (float timestamp, int position)
    {
        Timestamp = timestamp;
        Position = position;
    }
}
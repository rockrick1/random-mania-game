public class Note
{
    public float Time { get; }
    public float EndTime { get; }
    public int Position { get; }
    public bool IsLong { get; }
    
    public Note (float time, int position)
    {
        Time = time;
        Position = position;
        IsLong = false;
    }
    
    public Note (float time, float endTime, int position)
    {
        Time = time;
        EndTime = endTime;
        Position = position;
        IsLong = true;
    }
}
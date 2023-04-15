using System.Collections.Generic;

public class SongSettings : ISongSettings
{
    public string Id { get; set; }
    public float Bpm { get; set; }
    public float ApproachRate { get; set; }
    public float Difficulty { get; set; }
    public float StartingTime { get; set; }
    public List<Note> Notes { get; set; }
    
    public SongSettings ()
    {
        Notes = new List<Note>();
    }
}
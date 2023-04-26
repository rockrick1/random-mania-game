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
        Bpm = 60;
        ApproachRate = 1;
        Difficulty = 0;
        StartingTime = 0;
        Notes = new List<Note>();
    }

    public SongSettings Clone () =>
        new SongSettings
        {
            Id = Id,
            Bpm = Bpm,
            ApproachRate = ApproachRate,
            Difficulty = Difficulty,
            StartingTime = StartingTime,
            Notes = new List<Note>(Notes)
        };
}
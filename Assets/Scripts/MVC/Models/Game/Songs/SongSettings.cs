using System.Collections.Generic;
using UnityEngine;

public class SongSettings : ISongSettings
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string DifficultyName { get; set; }
    public float Bpm { get; set; }
    public float ApproachRate { get; set; }
    public float Difficulty { get; set; }
    public float StartingTime { get; set; }
    public Sprite Background { get; set; }
    public List<Note> Notes { get; private set; }
    
    public SongSettings ()
    {
        Bpm = 60;
        ApproachRate = 1;
        Notes = new List<Note>();
    }

    public SongSettings Clone () =>
        new SongSettings
        {
            Id = Id,
            Title = Title,
            Artist = Artist,
            DifficultyName = DifficultyName,
            Bpm = Bpm,
            ApproachRate = ApproachRate,
            Difficulty = Difficulty,
            StartingTime = StartingTime,
            Background = Background,
            Notes = new List<Note>(Notes)
        };
}
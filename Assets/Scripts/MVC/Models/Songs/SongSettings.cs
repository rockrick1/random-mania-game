using System.Collections.Generic;
using Newtonsoft.Json;

public class SongSettings : ISongSettings
{
    public double Bpm { get; set; }
    public float ApproachRate { get; set; }
    public List<Note> Notes { get; set; }
    
    public SongSettings ()
    {
        Notes = new List<Note>();
    }
}
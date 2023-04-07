using System.Collections.Generic;

public interface ISongSettings
{
    string Id { get; }
    double Bpm { get; }
    float ApproachRate { get; }
    float Difficulty { get; }
    float StartingTime { get; }
    List<Note> Notes { get; set; }
}
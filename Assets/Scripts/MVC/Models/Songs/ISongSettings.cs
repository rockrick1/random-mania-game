using System.Collections.Generic;

public interface ISongSettings
{
    double Bpm { get; }
    float ApproachRate { get; }
    float Difficulty { get; }
    List<Note> Notes { get; set; }
}
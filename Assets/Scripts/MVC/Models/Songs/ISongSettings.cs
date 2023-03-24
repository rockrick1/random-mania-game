using System.Collections.Generic;

public interface ISongSettings
{
    double Bpm { get; }
    float ApproachRate { get; }
    List<Note> Notes { get; set; }
}
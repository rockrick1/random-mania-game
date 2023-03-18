using System.Collections.Generic;

public interface ISongSettings
{
    float Bpm { get; }
    float ApproachRate { get; }
    IReadOnlyList<Note> Notes { get; }
}
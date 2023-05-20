using System.Collections.Generic;

public interface ISongSettings
{
    string Id { get; }
    string Title { get; }
    string Artist { get; }
    string DifficultyName { get; }
    float Bpm { get; }
    float ApproachRate { get; }
    float Difficulty { get; }
    float StartingTime { get; }
    List<Note> Notes { get; }
}
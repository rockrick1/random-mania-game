using System.Collections.Generic;
using UnityEngine;

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
    Sprite Background { get; set; }
    List<Note> Notes { get; }
}
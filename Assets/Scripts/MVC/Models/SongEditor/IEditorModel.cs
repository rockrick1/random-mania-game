using System;
using UnityEngine;

public interface IEditorModel : IDisposable
{
    void Initialize ();
    void ProcessSong (AudioClip clip);
}
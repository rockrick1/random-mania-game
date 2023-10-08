using System;

public interface IEditorHitsoundsModel : IDisposable
{
    event Action OnPlayHitsound;

    void Initialize ();
}
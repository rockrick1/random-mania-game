using System;
using System.Collections.Generic;

public interface ISongMenuModel : IDisposable
{
    void Initialize ();
    IReadOnlyList<ISongSettings> GetAllSongs ();
}
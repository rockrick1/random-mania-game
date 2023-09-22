using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/DefaultSongs")]
public class DefaultSongsDatabase : ScriptableObject
{
    [SerializeField] public List<string> songs;
}
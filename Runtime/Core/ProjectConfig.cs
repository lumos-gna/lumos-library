using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace LumosLib
{
    public class ProjectConfig : ScriptableObject
    {
        [field: Title("Preload")]
        [field: SerializeField] public List<GameObject> PreloadObjects { get; private set; } = new();
    }
}

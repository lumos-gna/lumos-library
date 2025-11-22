using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TriInspector;
using UnityEngine.InputSystem;

namespace LumosLib
{
    public class ProjectConfig : ScriptableObject
    {
        public enum TableType
        {
            None,
            GoogleSheet,
            Local
        }
        
        [field: Title("Data")]
        [field: SerializeField] public TableType DataTableType { get; private set; }
        [field: SerializeField, HideIf("DataTableType", TableType.None)] public string TablePath { get; private set; }
        
        [field: PropertySpace(20f)]
        [field: Title("Input")]
        [field: SerializeField] public InputActionAsset InputAsset { get; private set; }
        
        
        [field: PropertySpace(20f)]
        [field: Title("Audio")]
        [field: SerializeField] public AudioMixer Mixer { get; private set; }


        [field: PropertySpace(20f)]
        [field: Title("Preload")]
        [field: SerializeField] public List<GameObject> PreloadObjects { get; private set; } = new();
    }
}

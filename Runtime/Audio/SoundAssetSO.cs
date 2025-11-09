using UnityEngine;
using UnityEngine.Audio;

namespace LumosLib
{
    [CreateAssetMenu(fileName = "SoundAsset", menuName = "[ ✨Lumos Lib Asset ]/Sound Asset")  ]
    public class SoundAssetSO : ScriptableObject
    {
        [field: SerializeField] public AudioMixerGroup MixerGroup { get; set; }
        [field: SerializeField] public AudioClip Clip { get; set; }
        [field: SerializeField] public float VolumeFactor { get; set; }
        [field: SerializeField] public bool IsLoop { get; set; }

        public virtual int GetID()
        {
            return name.GetHashCode();
            
        }
    }
}
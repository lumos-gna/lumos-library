using UnityEngine;
using UnityEngine.Audio;

namespace LumosLib
{
    public class SoundAssetSO : ScriptableObject
    {
        [field: SerializeField] public AudioMixerGroup MixerGroup { get; set; }
        [field: SerializeField] public AudioClip Clip { get; set; }
        [field: SerializeField] public float VolumeFactor { get; set; }
        [field: SerializeField] public bool IsLoop { get; set; }

    }
}
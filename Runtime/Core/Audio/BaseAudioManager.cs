using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace LumosLib
{
    public abstract class BaseAudioManager : MonoBehaviour, IPreInitializable, IAudioManager
    {
        #region >--------------------------------------------------- FIELD
        
        [SerializeField] protected BaseResourceManager _resourceManager;
        [SerializeField] protected BasePoolManager _poolManager;
        [SerializeField] protected AudioPlayer _audioPlayerPrefab;
        [SerializeField] protected AudioMixer _mixer;
        protected readonly Dictionary<string, SoundAsset> _assetResources = new();
        
        
        #endregion
        #region >--------------------------------------------------- INIT
        
        
        public virtual IEnumerator InitAsync()
        {
            var soundResources = _resourceManager.LoadAll<SoundAsset>("");
            
            foreach (var resource in soundResources)
            {
                _assetResources[resource.name] = resource;
            }

            GlobalService.Register<IAudioManager>(this);
            DontDestroyOnLoad(gameObject);
            
            yield break;
        }
        
        
        #endregion
        #region >--------------------------------------------------- SET
        
        
        public void SetVolume(string groupName, float volume)
        {
            _mixer.SetFloat(groupName, Mathf.Log10(volume) * 20f);
        }
        
        
        #endregion
        #region >--------------------------------------------------- PLAY
        
        
        public abstract void PlayBGM(int bgmType, string assetName);
        public abstract void PlaySFX(string assetName);
        
        
        #endregion
        #region >--------------------------------------------------- STOP
        
        
        public abstract void StopBGM(int bgmType);
        public abstract void StopSFXAll();
        public abstract void StopAll();
        
        
        #endregion
        #region >--------------------------------------------------- PAUSE
        
        
        public abstract void PauseBGM(int bgmType, bool enable);
        public abstract void PauseSFXAll(bool enable);
        public abstract void PauseAll(bool enable);
        
        
        #endregion
    }
}
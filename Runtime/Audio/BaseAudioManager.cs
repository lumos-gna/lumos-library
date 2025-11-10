using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace LumosLib
{
    public abstract class BaseAudioManager : MonoBehaviour, IPreInitialize, IAudioManager
    {
        #region >--------------------------------------------------- PROPERTIE

        
        public int PreInitOrder => (int)PreInitializeOrder.Audio;
        public abstract bool PreInitialized { get; protected set; }
        

        #endregion
        #region >--------------------------------------------------- FIELD
      
        protected AudioMixer _mixer;
        protected IPoolManager _poolManager;
        protected AudioPlayer _playerPrefab;
        protected Dictionary<int, AudioPlayer> _bgmPlayers = new();
        protected HashSet<AudioPlayer> _activePlayers = new();
        protected Dictionary<string, SoundAssetSO> _assetResources = new();
        
        
        #endregion
        #region >--------------------------------------------------- UNITY

        protected virtual void Awake()
        {
            _poolManager = Global.GetInternal<IPoolManager>();
            
            var resourceManager = Global.GetInternal<IResourceManager>();
            var resources = resourceManager.LoadAll<SoundAssetSO>("");
            
            foreach (var resource in resources)
            {
                _assetResources[resource.name] = resource;
                
                Debug.Log(resource.name);
            }

            _playerPrefab = PreInitializer.Instance.Config.AudioPlayerPrefab;
            if (_playerPrefab == null)
            {
                DebugUtil.LogError(" wrong audio player path ", " INIT FAIL ");
                return;
            }
            
            _mixer = PreInitializer.Instance.Config.Mixer;
            
            Global.Register<IAudioManager>(this);
            
            DontDestroyOnLoad(gameObject);
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
        
        
        protected void Play(string assetName, AudioPlayer player)
        {
            if (_assetResources.TryGetValue(assetName, out SoundAssetSO asset))
            {
                player.Play(asset);
                
                _activePlayers.Add(player);
            }
        }
        
        
        #endregion
        #region >--------------------------------------------------- STOP
    

        public abstract void StopBGM(int bgmType);
        public abstract void StopAll();
        
        
        #endregion
        #region >--------------------------------------------------- PAUSE


        public abstract void PauseBGM(int bgmType, bool enable);
        public abstract void PauseAll(bool enable);
        
        
        #endregion
        #region >--------------------------------------------------- POOL

        
        public void ReleaseAudioPlayer(AudioPlayer player)
        {
            _poolManager.Release(player);

            if (_activePlayers.Contains(player))
            {
                _activePlayers.Remove(player);
            }
        }
        
        
        #endregion
    }
}
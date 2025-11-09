namespace LumosLib
{
    public class AudioManager : BaseAudioManager
    {
        #region >--------------------------------------------------- PROPERTIES


        public override bool PreInitialized { get; protected set; } = true;
        

        #endregion
        #region >--------------------------------------------------- PLAY

        
        public override void PlayBGM(int bgmType, int assetId)
        {
            if (_bgmPlayers.TryGetValue(bgmType, out var containsPlayer))
            {
                Play(assetId, containsPlayer);
                return;
            }
            
            var bgmPlayer = _poolManager.Get(_playerPrefab);
            
            _bgmPlayers[bgmType] = bgmPlayer;
            
            Play(assetId, bgmPlayer);
        }
        
        public override void PlaySFX(int assetId)
        {
            Play(assetId, _poolManager.Get(_playerPrefab));
        }
        
        
        #endregion
        #region >--------------------------------------------------- STOP


        public override void StopBGM(int bgmType)
        {
            if (_bgmPlayers.TryGetValue(bgmType, out var containsPlayer))
            {
                containsPlayer.Stop();
            }
        }
        
        public override void StopAll()
        {
            foreach (var player in _activePlayers)
            {
                player.Stop();
            }
        }

        
        #endregion
        #region >--------------------------------------------------- PUASE

        
        public override void PauseBGM(int bgmType, bool enable)
        {
            if (_bgmPlayers.TryGetValue(bgmType, out var containsPlayer))
            {
                containsPlayer.Pause(enable);
            }
        }
        
        public override void PauseAll(bool enable)
        {
            foreach (var player in _activePlayers)
            {
                player.Pause(enable);
            }
        }
        
        
        #endregion
    }
}
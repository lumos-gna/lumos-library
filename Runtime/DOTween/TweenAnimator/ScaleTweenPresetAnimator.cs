using DG.Tweening;
using LumosLib;
using TriInspector;
using UnityEngine;

namespace DOTween.TweenAnimator
{
    public class ScaleTweenPresetAnimator : BaseTweenPresetAnimator<ScaleTweenPreset>
    {
        [PropertySpace(10f)]
        public override Tweener GetTweener(string key)
        {
            var preset = GetPreset(key);
            
            if (preset.GetUseInitScale())
            {
                transform.localScale = preset.GetInitScale();
            }
            
            return OnGetTweener(preset, transform.DOScale(preset.GetScale(), preset.GetDuration()));
        }
    }
}
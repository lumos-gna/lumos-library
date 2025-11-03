using UnityEngine.Events;

namespace Lumos.DevKit
{
    public interface IPoolable
    {
        public void OnGet();
        public void OnRelease();
    }
}


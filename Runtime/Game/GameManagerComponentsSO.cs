using UnityEngine;

namespace Lumos.DevPack
{
    [CreateAssetMenu(fileName = "GameManagerComponents", menuName = "Scriptable Objects/Game Manager Components",
        order = 0)]
    public class GameManagerComponentsSO : ScriptableObject
    {
        [field: SerializeField] public BaseGameComponent[] ComponentPrefabs { get; private set; }
    }
}
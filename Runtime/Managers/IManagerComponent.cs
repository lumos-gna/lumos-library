using UnityEngine;

public abstract class ManagerComponent : MonoBehaviour
{
    public abstract int Order { get; protected set; }
    public abstract bool IsInitialized { get; protected set; }
    public abstract void Init();
}
using UnityEngine;

public abstract class BaseSceneManager : MonoBehaviour
{
    private void Awake()
    {
        if (BaseGameManager.Instance == null)
        {
            var prefab = Resources.Load<BaseGameManager>(Constant.GAME_MANAGER);
            if (prefab != null)
                Instantiate(prefab);
        }

        Global.Register(this);
    }

    private void OnDestroy()
    {
        Global.Unregister(this);
    }
}
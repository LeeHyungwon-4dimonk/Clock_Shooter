using UnityEngine;

public static class Manager
{
    public static PoolManager Pool => PoolManager.Instance;
    public static GameManager Game => GameManager.Instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        PoolManager.CreateInstance();
        GameManager.CreateInstance();
    }
}

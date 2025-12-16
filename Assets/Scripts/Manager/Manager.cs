using UnityEngine;

public static class Manager
{
    public static PoolManager Pool => PoolManager.Instance;
    public static GameManager Game => GameManager.Instance;

    public static UIManager UI => UIManager.Instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        AssetLoaderProvider.Initialize(new ResourcesAssetLoader());
        PoolManager.CreateInstance();
        UIManager.CreateInstance();
        GameManager.CreateInstance();        
    }
}
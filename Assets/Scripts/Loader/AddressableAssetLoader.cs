#if ADDRESSABLES_ENABLED
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class AddressablesAssetLoader : IAssetLoader
{
    public async Task<T> LoadAsync<T>(string key) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadAssetAsync<T>(key);
        return await handle.Task;
    }

    public void Release<T>(T asset) where T : UnityEngine.Object
    {
        Addressables.Release(asset);
    }
}
#endif
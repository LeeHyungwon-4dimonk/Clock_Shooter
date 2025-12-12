using System.Threading.Tasks;
using UnityEngine;

public class ResourcesAssetLoader : IAssetLoader
{
    public async Task<T> LoadAsync<T>(string key) where T : Object
    {
        var asset = Resources.Load<T>(key);
        return await Task.FromResult(asset);
    }

    public void Release<T>(T asset) where T : Object
    {

    }
}
using System.Threading.Tasks;
using UnityEngine;

public interface IAssetLoader
{
    Task<T> LoadAsync<T>(string key) where T : Object;
    void Release<T>(T asset) where T : Object;
}

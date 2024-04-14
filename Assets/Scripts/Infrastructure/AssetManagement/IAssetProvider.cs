using Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : ISystem
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Vector2 at);
        public GameObject Instantiate(string path, Vector2 at, Transform parent);
    }
}
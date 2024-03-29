using Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreateHero(GameObject at);
        public void CreateHud();
    }
}
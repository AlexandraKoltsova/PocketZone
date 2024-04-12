using Services;
using StaticData.Mutant;
using StaticData.Player;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : ISystem
    {
        public GameObject CreatePlayer(PlayerStaticData playerData, GameObject at);
        public GameObject CreateHUD();
        public GameObject CreateMutant(MutantStaticData mutantData);
        public void CreateSpawner(Vector3 at);
    }
}
using Infrastructure.Factory;
using Mutant;
using UnityEngine;

namespace Services.Spawner
{
    public class LootSpawnSystem : ILootSpawnSystem
    {
        public MutantDeath EnemyDeath;
        private IGameFactory _gameFactory;

        public LootSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
        }

        // private void Start()
        // {
        //     EnemyDeath.Happened += SpawnLoot;
        // }

        public void InitCollectibleHolder()
        {
            GameObject spawner = _gameFactory.CreateCollectibleHolder(Vector3.zero);
        }
        
        public void SpawnLoot()
        {
            //GameObject loot = _gameFactory.CreateLoot();
        }
    }
}
using System.Collections.Generic;
using Infrastructure.Factory;
using Logic.Inventory;
using Services.StaticData;
using StaticData.Inventory;
using UI.Inventory;
using UnityEngine;

namespace Services.Spawner
{
    public class LootSpawnSystem : ILootSpawnSystem
    {
        private List<GameObject> _loots = new List<GameObject>();
        
        private IGameFactory _gameFactory;
        private IStaticDataSystem _staticData;
        
        public LootSpawnSystem()
        {
            _gameFactory = SystemsManager.Get<IGameFactory>();
            _staticData = SystemsManager.Get<IStaticDataSystem>();
        }
        
        public void InitLootHolder()
        {
            GameObject lootHolder = _gameFactory.CreateLootHolder(Vector2.zero);
            
            foreach (ItemConfig config in _staticData.GetItemConfigs())
                SpawnLoot(config, lootHolder.transform);
        }

        private void SpawnLoot(ItemConfig config, Transform lootHolder)
        {
            ItemData itemData = new ItemData();
            itemData.SetData(config);
            
            GameObject loot = _gameFactory.CreateLoot(Vector2.zero, lootHolder);
            loot.GetComponent<DroppedItemView>().SetData(itemData);
            loot.gameObject.SetActive(false);
            
            _loots.Add(loot);
        }

        public List<GameObject> GetLootGameObjects()
        {
            return _loots;
        }
    }
}
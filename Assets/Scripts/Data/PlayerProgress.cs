using System;
using Data.PlayerStatus;
using Data.Position;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public Health Health;
        public Stats Stats;
        public KillData KillData;
        
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            Health = new Health();
            Stats = new Stats();
            KillData = new KillData();
        }
    }
}
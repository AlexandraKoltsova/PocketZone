using System;

namespace Data.Position
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        
        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}
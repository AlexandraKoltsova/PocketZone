using System;

namespace Data
{
    [Serializable]
    public class PlayerStats
    {
        public int CurrentHP;
        public int MaxHP;
        
        public int MoveSpeed;
        public float AimRadius;
        
        public int Damage;
        public int BulletSpeed;
    }
}
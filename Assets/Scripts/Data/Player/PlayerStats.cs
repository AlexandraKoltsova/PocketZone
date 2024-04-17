using System;

namespace Data.Player
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
        
        public int BulletCurrent;
        public int BulletMax;
    }
}
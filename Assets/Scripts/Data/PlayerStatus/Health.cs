using System;

namespace Data.PlayerStatus
{
    [Serializable]
    public class Health
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}
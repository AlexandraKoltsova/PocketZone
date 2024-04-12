using System;

namespace Data.PlayerStatus
{
    [Serializable]
    public class Health
    {
        public int CurrentHP;
        public int MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}
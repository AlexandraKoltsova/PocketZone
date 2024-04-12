using System;

namespace Logic
{
    public interface IHealth
    {
        public event Action HealthChanged;

        public int Current { get; set; }
        public int Max { get; set; }

        public void TakeDamage(int damage);
    }
}
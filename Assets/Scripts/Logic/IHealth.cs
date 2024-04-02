using System;

namespace Logic
{
    public interface IHealth
    {
        public event Action HealthChanged;

        public float Current { get; set; }
        public float Max { get; set; }

        public void TakeDamage(float damage);
    }
}
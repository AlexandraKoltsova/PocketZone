using System;

namespace Logic
{
    public interface IProjectile
    {
        public event Action ProjectileCountChanged;

        public int Current { get; set; }
        public int Max { get; set; }
    }
}
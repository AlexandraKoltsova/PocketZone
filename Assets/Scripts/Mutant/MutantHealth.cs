using System;
using Logic;
using UnityEngine;

namespace Mutant
{
    public class MutantHealth : MonoBehaviour, IHealth
    {
        private MutantAnimator _animator;

        public event Action HealthChanged;
        
        private int _current;
        private int _max;

        public void Init(MutantAnimator animator)
        {
            _animator = animator;
        }

        public void Construct(int health)
        {
            Current = health;
            Max = health;
        }
        
        public int Current
        {
            get => _current;
            set => _current = value;
        }

        public int Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}
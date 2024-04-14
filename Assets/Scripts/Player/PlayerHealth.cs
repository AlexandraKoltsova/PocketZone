using System;
using Logic;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private PlayerAnimator _animator;

        public int _currentHp;
        public int _maxHp;
        public event Action HealthChanged;

        public int Current
        {
            get => _currentHp;
            set
            {
                if (_currentHp != value)
                {
                    _currentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public int Max
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        public void Init(PlayerAnimator animator)
        {
            _animator = animator;
        }
        
        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
        }
    }
}
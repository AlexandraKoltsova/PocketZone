using System;
using Data;
using Data.PlayerStatus;
using Logic;
using Services.PersistentProgress;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        private PlayerAnimator _animator;

        public event  Action HealthChanged;
        private Health _health;

        public float Current
        {
            get => _health.CurrentHP;
            set
            {
                if (_health.CurrentHP != value)
                {
                    _health.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _health.MaxHP;
            set => _health.MaxHP = value;
        }

        public void Init(PlayerAnimator animator)
        {
            _animator = animator;
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _health = progress.Health;
            HealthChanged?.Invoke();
        }

        public void SaveProgress(PlayerProgress progress)
        {
            progress.Health.CurrentHP = Current;
            progress.Health.MaxHP = Max;
        }
        
        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
        }
    }
}
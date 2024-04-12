using System;
using Data.PlayerStatus;
using Infrastructure.AssetManagement;
using Logic;
using Services.SaveLoad;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, ILoadSystem, IHealth
    {
        private PlayerAnimator _animator;
        private Health _health;

        private int _currentHp;
        private int _maxHp;
        public event Action HealthChanged;
        public string SaveKey { get; } = AssetsAddress.PlayerHealthSaveKey;

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
            _health = new Health();
        }
        
        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
        }

        public SaveData GetSaveData()
        {
            var json = JsonUtility.ToJson(_health);
            
            var data = new SaveData
            {
                Key = AssetsAddress.PlayerHealthSaveKey,
                Json = json
            };
            
            _health.CurrentHP = _currentHp;
            _health.MaxHP = _maxHp;
            
            return data;
        }

        public void LoadSaveData(SaveData saveData)
        {
            if (saveData == null || string.IsNullOrEmpty(saveData.Json)) return;

            var data = JsonUtility.FromJson<Health>(saveData.Json);
            if (data == null) return;
            _health = data;

            _currentHp = _health.CurrentHP;
            _maxHp = _health.MaxHP;
            HealthChanged?.Invoke();
        }
    }
}
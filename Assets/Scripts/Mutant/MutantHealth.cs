using System;
using Logic;
using UnityEngine;

namespace Mutant
{
    [RequireComponent(typeof(MutantHealth))]
    public class MutantHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private MutantAnimator _animator;

        public event Action HealthChanged;
        
        [SerializeField] private float _current;
        [SerializeField] private float _max;


        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}
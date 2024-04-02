using System;
using System.Collections;
using UnityEngine;

namespace Mutant
{
    public class MutantDeath : MonoBehaviour
    {
        [SerializeField] private MutantHealth _health;
        [SerializeField] private MutantAnimator _animator;

        public event Action Happened;

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;

            _animator.PlayDeath();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
        }
    }
}
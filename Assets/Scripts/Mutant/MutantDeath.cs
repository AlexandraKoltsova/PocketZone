using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantDeath : MonoBehaviour
    {
        [SerializeField] private MutantHealth _health;
        [SerializeField] private MutantAnimator _animator;
        [SerializeField] private Aggro _aggro;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AgentMoveToPlayer _agentMove;

        public event Action Happened;
        public event Action<GameObject> OnDead;

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

            _agentMove.enabled = false;
            _aggro.enabled = false;
            _animator.PlayDeath();

            OnDead?.Invoke(gameObject);
            
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
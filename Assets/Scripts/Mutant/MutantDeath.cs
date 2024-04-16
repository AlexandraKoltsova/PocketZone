using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantDeath : MonoBehaviour
    {
        private MutantHealth _health;
        private MutantAnimator _animator;
        private Aggro _aggro;
        private AgentMoveToPlayer _agentMove;

        public event Action<Transform> Happened;
        public event Action OnDead;
        
        public void Init(MutantHealth health, MutantAnimator animator, Aggro aggro, NavMeshAgent agent, AgentMoveToPlayer agentMove)
        {
            _health = health;
            _animator = animator;
            _aggro = aggro;
            _agentMove = agentMove;
        }  
        
        public void Startup()
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

            OnDead?.Invoke();
            
            StartCoroutine(DestroyTimer());

            Happened?.Invoke(transform);
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
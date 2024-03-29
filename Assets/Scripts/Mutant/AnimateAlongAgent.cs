using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(MutantAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVeloсity = 0.1f;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private MutantAnimator _animator;

        public void Update()
        {
            if (ShouldMove())
                _animator.Move();
            else
                _animator.StopMoving();
        }
        
        private bool ShouldMove()
        {
            return _agent.velocity.magnitude > MinimalVeloсity && _agent.remainingDistance > _agent.radius;
        }
    }
}
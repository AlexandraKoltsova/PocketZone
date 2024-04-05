using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVeloсity = 0.1f;

        private NavMeshAgent _agent;
        private MutantAnimator _animator;

        public void Init(NavMeshAgent agent, MutantAnimator animator)
        {
            _agent = agent;
            _animator = animator;
        }  
        
        public void Tick()
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
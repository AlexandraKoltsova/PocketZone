using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;

        private NavMeshAgent _agent;
        private Transform _mesh;

        private Transform _playerTransform;
        
        public void Construct(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void Init(NavMeshAgent agent, Transform mesh)
        {
            _agent = agent;
            _mesh = mesh;
            
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        public void Tick(bool canMove)
        {
            if (!canMove)
            {
                return;
            }
            
            if (Initialized() && HeroNotReached())
            {
                _agent.destination = _playerTransform.position;
            }
            
            if (_agent.velocity.x < Constants.Epsilon)
            {
                ChangeDirection(-1);
            }
            else
            {
                ChangeDirection(1);
            }
        }

        private void ChangeDirection(int x)
        {
            _mesh.transform.localScale = new Vector3(x, 1, 1);
        }

        private bool HeroNotReached()
        {
            return Vector2.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;
        }

        private bool Initialized()
        {
            return _playerTransform != null;
        }
    }
}
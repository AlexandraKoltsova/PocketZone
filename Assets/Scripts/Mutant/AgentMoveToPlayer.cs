using DefaultNamespace;
using Infrastructure.Factory;
using Services;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _mesh;

        private Transform _playerTransform;
        private IGameFactory _gameFactory;
        
        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            
            if (HeroExists())
                InitializeHeroTransform();
            else
                _gameFactory.PlayerCreated += InitializeHeroTransform;
        }

        private void Update()
        {
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
        
        private bool HeroExists()
        {
            return _gameFactory.PlayerGameObject != null;
        }
        
        private void InitializeHeroTransform()
        {
            _playerTransform = _gameFactory.PlayerGameObject.transform;
        }

        private bool Initialized()
        {
            return _playerTransform != null;
        }
    }
}
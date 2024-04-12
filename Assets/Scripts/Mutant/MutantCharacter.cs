using System;
using Logic;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent), typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(AgentMoveToPlayer), typeof(AnimateAlongAgent), typeof(MutantAnimator))]
    [RequireComponent(typeof(MutantHealth), typeof(MutantDeath), typeof(MutantAttack))]
    [RequireComponent(typeof(AttackZone), typeof(Aggro))]
    public class MutantCharacter : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Animator _animator;
        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _mesh;

        [Header("Dependencies")] 
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] private AnimateAlongAgent _animateAlongAgent;
        [SerializeField] private MutantAnimator _mutantAnimator;
        [SerializeField] private MutantHealth _mutantHealth;
        [SerializeField] private MutantDeath _mutantDeath;
        [SerializeField] private MutantAttack _mutantAttack;
        [SerializeField] private ZoneObserver _zoneAttack;
        [SerializeField] private ZoneObserver _zoneAggro;
        [SerializeField] private AttackZone _attackZone;
        [SerializeField] private Aggro _aggro;

        [Header("Properties")]
        public float MutantId;
        public event Action<MutantCharacter> OnDead;

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<CapsuleCollider2D>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _agentMoveToPlayer = GetComponent<AgentMoveToPlayer>();
            _animateAlongAgent = GetComponent<AnimateAlongAgent>();
            _mutantAnimator = GetComponent<MutantAnimator>();
            _mutantHealth = GetComponent<MutantHealth>();
            _mutantDeath = GetComponent<MutantDeath>();
            _mutantAttack = GetComponent<MutantAttack>();
            _attackZone = GetComponent<AttackZone>();
            _aggro = GetComponent<Aggro>();
        }

        private void Awake()
        {
            _mutantAnimator.Init(_animator);
            _mutantHealth.Init(_mutantAnimator);
            _agentMoveToPlayer.Init(_navMeshAgent, _mesh);
            _animateAlongAgent.Init(_navMeshAgent, _mutantAnimator);
            _mutantAttack.Init(_mutantAnimator);
            _attackZone.Init(_mutantAttack, _zoneAttack);
            _aggro.Init(_zoneAggro);
            _mutantDeath.Init(_mutantHealth, _mutantAnimator, _aggro, _navMeshAgent, _agentMoveToPlayer);

            _mutantDeath.OnDead += MutantDead;
        }

        private void MutantDead()
        {
            OnDead?.Invoke(this);
        }

        private void Start()
        {
            _aggro.Startup();
            _attackZone.Startup();
            _mutantDeath.Startup();
        }

        private void Update()
        {
            _agentMoveToPlayer.Tick(_aggro.Tick());
            _animateAlongAgent.Tick();
            _mutantAttack.Tick();
        }

        private void OnDisable()
        { 
            _mutantDeath.OnDead -= MutantDead;
        }
    }
}
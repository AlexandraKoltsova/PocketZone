using Logic;
using Services;
using Services.Input;
using Services.SaveLoad;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimator), typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerDeath), typeof(PlayerAim), typeof(AimZone))]
    [RequireComponent(typeof(PlayerShooting))]
    public class PlayerCharacter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private CircleCollider2D _aimCollider;
        [SerializeField] private Transform _mesh;
        [SerializeField] private Transform _aimPoint;
        [SerializeField] private Transform _gunEndPointPosition;
        
        [Header("Dependencies")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerDeath _playerDeath;
        [SerializeField] private PlayerAim _playerAim;
        [SerializeField] private AimZone _aimZone;
        [SerializeField] private PlayerShooting _playerShooting;
        [SerializeField] private ZoneObserver _zoneObserver;

        [Header("Bullets")]
        [SerializeField] private int _damage;
        [SerializeField] private int _bulletSpeed;
        [SerializeField] private GameObject[] _bullets;

        private IInputSystem _inputSystem;
        private ISaveLoadSystem _saveLoadSystem;

        private void OnValidate()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerDeath = GetComponent<PlayerDeath>();
            _playerAim = GetComponent<PlayerAim>();
            _aimZone = GetComponent<AimZone>();
            _playerShooting = GetComponent<PlayerShooting>();
            _zoneObserver = GetComponentInChildren<ZoneObserver>();
        }

        public void ConstructSystems()
        {
            _inputSystem = SystemsManager.Get<IInputSystem>();
            _saveLoadSystem = SystemsManager.Get<ISaveLoadSystem>();
        }
        
        public void ConstructAttack(int damage, int bulletSpeed)
        {
            _damage = damage;
            _bulletSpeed = bulletSpeed;
        }
        
        private void Awake()
        {
            ConstructSystems();
            
            foreach (GameObject bullet in _bullets) 
                bullet.GetComponent<Bullet>().Construct(_damage, _bulletSpeed);
            
            _playerMovement.Init(_inputSystem, _saveLoadSystem, _rb, _collider, _mesh);
            _aimZone.Init(_zoneObserver, _aimCollider);
            _playerAim.Init(_inputSystem, _aimZone, _aimPoint, _gunEndPointPosition);
            _playerAnimator.Init(_animator, _rb);
            _playerHealth.Init(_playerAnimator);
            _playerDeath.Init(_playerAnimator, _playerHealth);
            _playerShooting.Init(_playerAim, _bullets);
        }
        
        private void Start()
        {
            _playerDeath.Startup();
            _aimZone.Startup();
            _playerShooting.Startup();
        }
        
        private void Update()
        {
            _playerAnimator.Tick();
            _playerAim.Tick(!_playerDeath.PlayerDead());
            _aimZone.Tick();
        }

        private void FixedUpdate()
        {
            _playerMovement.FixedTick(!_playerDeath.PlayerDead());
        }
    }
}
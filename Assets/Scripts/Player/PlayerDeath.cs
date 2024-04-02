using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAim _playerAim;
        
        private bool _isDead;
        
        private void Start()
        {
            _playerHealth.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && _playerHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;

            _movement.enabled = false;
            _playerAim.enabled = false;
            _animator.PlayDeath();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= HealthChanged;
        }
    }
}
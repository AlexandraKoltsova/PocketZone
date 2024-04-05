using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        private PlayerAnimator _animator;
        private PlayerHealth _playerHealth;
        
        private bool _isDead;

        public void Init(PlayerAnimator animator, PlayerHealth playerHealth)
        {
            _animator = animator;
            _playerHealth = playerHealth;
        }
        
        public void Startup()
        {
            _playerHealth.HealthChanged += HealthChanged;
        }

        public bool PlayerDead()
        {
            if (_isDead)
            {
                return true;
            }
            return false;
        }
        
        private void HealthChanged()
        {
            if (!_isDead && _playerHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _animator.PlayDeath();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= HealthChanged;
        }
    }
}
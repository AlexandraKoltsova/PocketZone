using Player;
using UnityEngine;

namespace UI.HUD
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        
        private PlayerHealth _playerHealth;
        
        public void Construct(PlayerHealth health)
        {
            _playerHealth = health;

            _playerHealth.HealthChanged += UpdateHpBar;
        }
        
        private void UpdateHpBar()
        {
            _healthBar.SetValue(_playerHealth.Current, _playerHealth.Max);
        }
        
        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= UpdateHpBar;
        }
    }
}
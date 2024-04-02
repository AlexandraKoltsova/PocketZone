using Logic;
using UnityEngine;

namespace UI.HUD
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        
        private IHealth _health;
        
        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHealthBar;
        }
        
        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }
        
        private void UpdateHealthBar()
        {
            _healthBar.SetValue(_health.Current, _health.Max);
        }
        
        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHealthBar;
        }
    }
}
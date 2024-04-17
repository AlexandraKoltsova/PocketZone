using Logic;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _projectilesText;

        private IProjectile _projectile;

        public void Construct(IProjectile projectile)
        {
            _projectile = projectile;
            _projectile.ProjectileCountChanged += UpdateProjectile;
        }

        public void UpdateProjectile()
        {
            _projectilesText.text = $"{_projectile.Current.ToString()}/{_projectile.Max.ToString()}";
        }

        private void OnDestroy()
        {
            _projectile.ProjectileCountChanged -= UpdateProjectile;
        }
    }
}
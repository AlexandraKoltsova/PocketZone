using System.Linq;
using Infrastructure.Factory;
using Services;
using UnityEngine;

namespace Mutant
{
    [RequireComponent(typeof(MutantAnimator))]
    public class MutantAttack : MonoBehaviour
    {
        [SerializeField] private MutantAnimator _animator;

        [SerializeField] private float _attackColldown = 3f;
        [SerializeField] private float _cLeavage = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;

        private float _currentAttackColldown;
        private bool _isAttacking;
        private bool _attackIsActive;

        private int _layerMask;
        private Collider2D[] _hits = new Collider2D[1];

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            if (Hit(out Collider2D hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _cLeavage, 1);
            }
        }

        private bool Hit(out Collider2D hit)
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(StartPoint(), _cLeavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private Vector2 StartPoint()
        {
            return new Vector2(transform.position.x * _effectiveDistance, transform.position.y * _effectiveDistance + 1f);
        }


        private void OnAttackEnded()
        {
            _currentAttackColldown = _attackColldown;

            _isAttacking = false;
        }

        private bool CooldownIsUp()
        {
            return _currentAttackColldown <= 0;
        }

        private void StartAttack()
        {
            //transform.LookAt(_playerTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool CanAttack()
        {
            return _attackIsActive && !_isAttacking && CooldownIsUp();
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _currentAttackColldown -= Time.deltaTime;
            }
        }
    }
}
using System.Linq;
using Logic;
using UnityEngine;

namespace Mutant
{
    public class MutantAttack : MonoBehaviour
    {
        private MutantAnimator _animator;

        private int _damage = 10;
        private float _attackColldown = 3f;
        private float _CLeavage = 0.5f;
        private float _effectiveDistance = 0.5f;

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

        public void Init(MutantAnimator animator)
        {
            _animator = animator;
            
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        public void Construct(int damage, float attackCooldown, float CLeavag, float effectiveDistance)
        {
            _damage = damage;
            _attackColldown = attackCooldown;
            _CLeavage = CLeavag;
            _effectiveDistance = effectiveDistance;
        }
        
        public void Tick()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            if (Hit(out Collider2D hit) && hit.TryGetComponent(out IHealth playerHealth))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _CLeavage, 1);
                playerHealth.TakeDamage(_damage);
            }
        }

        private bool Hit(out Collider2D hit)
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(StartPoint(), _CLeavage, _hits, _layerMask);

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
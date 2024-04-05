using System.Linq;
using Logic;
using UnityEngine;

namespace Mutant
{
    public class MutantAttack : MonoBehaviour
    {
        private MutantAnimator _animator;

        public float Damage = 10f;
        public float AttackColldown = 3f;
        public float CLeavage = 0.5f;
        public float EffectiveDistance = 0.5f;

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
                PhysicsDebug.DrawDebug(StartPoint(), CLeavage, 1);
                playerHealth.TakeDamage(Damage);
            }
        }

        private bool Hit(out Collider2D hit)
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(StartPoint(), CLeavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private Vector2 StartPoint()
        {
            return new Vector2(transform.position.x * EffectiveDistance, transform.position.y * EffectiveDistance + 1f);
        }


        private void OnAttackEnded()
        {
            _currentAttackColldown = AttackColldown;

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
using System;
using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {
        private AimZone _aimZone;
        private Transform _aimPoint;
        private Transform _gunEndPointPosition;
        
        private Transform _target;
        private bool _isAiming;
        
        private IInputService _inputService;
        
        public event Action<OnShootEvent> OnShoot;

        public class OnShootEvent
        {
            public Vector3 GunEndPointPosition;
            public Vector3 ShootPosition;

        }
        
        public void Init(IInputService inputService, AimZone aimZone, Transform aimPoint, Transform gunEndPointPosition)
        {
            _aimZone = aimZone;
            _aimPoint = aimPoint;
            _gunEndPointPosition = gunEndPointPosition;
            _inputService = inputService;
            
            _aimZone.GetTarget += SetTarget;
            _aimZone.TargetEnable += TargetEnable;
            _aimZone.TargetDisable += TargetDisable;
        }

        public void Tick(bool canAim)
        {
            if (!canAim)
            {
                return;
            }
            
            if (_isAiming)
            {
                HandleShooting();
            }
        }

        private void TargetEnable()
        {
            _isAiming = true;
        }

        private void TargetDisable()
        {
            _isAiming = false;
            ResetAim();
        }

        private void SetTarget(Transform transform)
        {
            _target = transform;
            HandleAiming(_target.position);
        }

        private void HandleAiming(Vector3 target)
        {
            Vector3 aimDirection = (target - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _aimPoint.eulerAngles = new Vector3(0, 0, angle);

            HandleScale(angle);
        }

        private void ResetAim()
        {
            _aimPoint.eulerAngles = new Vector3(0, 0, 0);
            HandleScale(0);
        }

        private void HandleScale(float angle)
        {
            Vector3 localScale = Vector3.one;
            if (angle > 90 || angle < -90)
            {
                localScale.y = -1f;
            }
            else
            {
                localScale.y = 1f;
            }
            _aimPoint.localScale = localScale;
        }

        private void HandleShooting()
        {
            if (_inputService.IsAttackButton())
            {
                OnShoot?.Invoke(new OnShootEvent
                {
                    GunEndPointPosition = _gunEndPointPosition.position,
                    ShootPosition = _target.position,
                });
            }
        }

        private void OnDisable()
        {
            _aimZone.GetTarget -= SetTarget;
            _aimZone.TargetEnable -= TargetEnable;
            _aimZone.TargetDisable -= TargetDisable;
        }
    }
}
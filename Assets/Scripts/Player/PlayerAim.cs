using Services;
using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private AimZone _aimZone;
        [SerializeField] private Transform _aimPoint;

        private bool _isAiming;
        
        private IInputService _inputService;
        
        private void Awake()
        {
            _aimZone.GetTarget += SetTarget;
            _aimZone.TargetEnable += TargetEnable;
            _aimZone.TargetDisable += TargetDisable;

            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
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
            HandleAiming(transform.position);
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
                Debug.Log("Shoot");
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
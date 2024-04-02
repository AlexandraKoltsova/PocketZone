using System;
using System.Collections.Generic;
using Data;
using Data.PlayerStatus;
using Logic;
using Services.PersistentProgress;
using UnityEngine;

namespace Player
{
    public class AimZone : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private ZoneObserver _triggerObserver;
        [SerializeField] private CircleCollider2D _collider;
        
        private List<Transform> _targetsList = new List<Transform>();
        private Transform _currentTarget;
        
        private float _minDistance = Mathf.Infinity;

        public event Action<Transform> GetTarget;
        public event Action TargetEnable, TargetDisable;
        
        private Stats _stats;

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.Stats;
        }

        private void Start()
        {
            SetRadiusZone();
            
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            
            _targetsList.Clear();
        }

        private void Update()
        {
            if (_targetsList.Count == 0)
            {
                TargetDisable?.Invoke();
                return;
            }
            
            TargetEnable?.Invoke();
            
            _currentTarget = SetNearestTarget();
            GetTarget?.Invoke(_currentTarget);
        }

        private void SetRadiusZone()
        {
            _collider.radius = _stats.DamageRadius;
        }

        private Transform SetNearestTarget()
        {
            for (int i = 0; i < _targetsList.Count; i++)
            {
                var distance = Vector3.Distance(_targetsList[i].position, transform.position);

                if (distance < _minDistance)
                {
                    _minDistance = distance;
                    
                    return _targetsList[i];
                }
            }

            return _currentTarget;
        }

        private void TriggerEnter(Collider2D obj)
        {
            if (obj.TryGetComponent(out IHealth mutantHealth))
            {
                _targetsList.Add(obj.transform);
            }
        }

        private void TriggerExit(Collider2D obj)
        {
            if (obj.TryGetComponent(out IHealth mutantHealth))
            {
                _targetsList.Remove(obj.transform);
            }
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }
    }
}
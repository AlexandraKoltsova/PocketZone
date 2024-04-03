using System;
using System.Collections.Generic;
using Data;
using Data.PlayerStatus;
using Logic;
using Mutant;
using Services.PersistentProgress;
using UnityEngine;

namespace Player
{
    public class AimZone : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private ZoneObserver _triggerObserver;
        [SerializeField] private CircleCollider2D _collider;
        
        private List<UniqueId> _targetsList = new List<UniqueId>();
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
            Debug.Log(_targetsList.Count);
            
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
                Vector3 position = _targetsList[i].gameObject.transform.position;
                float distance = Vector3.Distance(position, transform.position);

                if (distance < _minDistance)
                {
                    _minDistance = distance;
                    
                    return _targetsList[i].gameObject.transform;
                }
            }

            return _currentTarget;
        }

        private void TriggerEnter(Collider2D obj)
        {
            if (obj.TryGetComponent(out UniqueId mutantId))
            {
                if (_targetsList.Exists(t => t == mutantId))
                {
                    return;
                }

                _targetsList.Add(mutantId);
                mutantId.GetComponent<MutantDeath>().OnDead += MutantOnDead;
            }
        }

        private void MutantOnDead(GameObject mutant)
        {
            for (int i = 0; i < _targetsList.Count; i++)
            {
                if (_targetsList[i].Id == mutant.GetComponent<UniqueId>().Id)
                {
                    _targetsList.Remove(_targetsList[i]);
                    _currentTarget = null;
                    _minDistance = Mathf.Infinity;
                }
            }
        }

        private void TriggerExit(Collider2D obj)
        {
            if (obj.TryGetComponent(out UniqueId mutantId))
            {
                for (int i = 0; i < _targetsList.Count; i++)
                {
                    if (_targetsList[i].Id == mutantId.Id)
                    {
                        _targetsList[i].GetComponent<MutantDeath>().OnDead -= MutantOnDead;
                        _targetsList.Remove(_targetsList[i]);
                        _currentTarget = null;
                        _minDistance = Mathf.Infinity;
                    }
                }
            }
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }
    }
}
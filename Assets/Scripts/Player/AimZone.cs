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
        private ZoneObserver _triggerObserver;
        private CircleCollider2D _collider;
        private Stats _stats;

        private List<MutantCharacter> _targetsList = new List<MutantCharacter>();
        private Transform _currentTarget;

        private float _minDistance = Mathf.Infinity;

        public event Action<Transform> GetTarget;
        public event Action TargetEnable, TargetDisable;

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.Stats;
        }

        public void Init(ZoneObserver triggerObserver, CircleCollider2D collider)
        {
            _triggerObserver = triggerObserver;
            _collider = collider;
        }
        
        public void Startup()
        {
            SetAimRadius();
            
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            
            _targetsList.Clear();
        }

        public void Tick()
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

        private void SetAimRadius()
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

        private void TriggerEnter(Collider2D collider)
        {
            if (collider.TryGetComponent(out MutantCharacter mutant))
            {
                if (_targetsList.Exists(t => t.MutantId == mutant.MutantId))
                {
                    return;
                }

                _targetsList.Add(mutant);
                mutant.OnDead += MutantOnDead;
            }
        }

        private void MutantOnDead(MutantCharacter mutant)
        {
            for (int i = 0; i < _targetsList.Count; i++)
            {
                if (_targetsList[i].MutantId == mutant.MutantId)
                {
                    _targetsList.Remove(_targetsList[i]);
                    _currentTarget = null;
                    _minDistance = Mathf.Infinity;
                }
            }
        }

        private void TriggerExit(Collider2D collider)
        {
            if (collider.TryGetComponent(out MutantCharacter mutant))
            {
                for (int i = 0; i < _targetsList.Count; i++)
                {
                    if (_targetsList[i].MutantId == mutant.MutantId)
                    {
                        _targetsList[i].OnDead -= MutantOnDead;
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
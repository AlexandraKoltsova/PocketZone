using Logic;
using UnityEngine;

namespace Mutant
{
    public class AttackZone : MonoBehaviour
    {
        [SerializeField] private MutantAttack _attack;
        [SerializeField] private ZoneObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            
            _attack.DisableAttack();
        }

        private void TriggerEnter(Collider2D obj)
        {
            _attack.EnableAttack();
        }

        private void TriggerExit(Collider2D obj)
        {
            _attack.DisableAttack();
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }
    }
}
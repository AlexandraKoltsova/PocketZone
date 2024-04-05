using Logic;
using UnityEngine;

namespace Mutant
{
    public class AttackZone : MonoBehaviour
    {
        private MutantAttack _attack;
        private ZoneObserver _triggerObserver;

        public void Init(MutantAttack attack, ZoneObserver triggerObserver)
        {
            _attack = attack;
            _triggerObserver = triggerObserver;
        }
        
        public void Startup()
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
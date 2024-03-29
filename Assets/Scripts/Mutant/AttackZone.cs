using UnityEngine;

namespace Mutant
{
    [RequireComponent(typeof(MutantAttack))]
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
    }
}
using UnityEngine;

namespace Mutant
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private AggroZoneObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _followAgent;
        
        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            
            SwitchFollowOff();
        }

        private void TriggerExit(Collider2D obj)
        {
            SwitchFollowOff();
        }

        private void TriggerEnter(Collider2D obj)
        {
            SwitchFollowOn();
        }
        
        private void SwitchFollowOn()
        {
            _followAgent.enabled = true;
        }
        
        private void SwitchFollowOff()
        {
            _followAgent.enabled = false;
        }
    }
}
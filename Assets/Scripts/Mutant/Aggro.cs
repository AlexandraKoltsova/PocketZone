using Logic;
using UnityEngine;

namespace Mutant
{
    public class Aggro : MonoBehaviour
    {
        private ZoneObserver _triggerObserver;
        private AgentMoveToPlayer _followAgent;
        private bool _canMove;
        
        public void Init(ZoneObserver triggerObserver, AgentMoveToPlayer followAgent)
        {
            _triggerObserver = triggerObserver;
            _followAgent = followAgent;
        }  
        
        public void Startup()
        {
            _triggerObserver.TriggerStay += TriggerStay;
            _triggerObserver.TriggerExit += TriggerExit;
            
            _canMove = false;
        }

        public bool Tick()
        {
            if (_canMove)
            {
                return true;
            }
            return false;
        }
        
        private void TriggerStay(Collider2D obj)
        {
            _canMove = true;
        }

        private void TriggerExit(Collider2D obj)
        {
            _canMove = false;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerStay -= TriggerStay;
            _triggerObserver.TriggerExit -= TriggerExit;
        }
    }
}
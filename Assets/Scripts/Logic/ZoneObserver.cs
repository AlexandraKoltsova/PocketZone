using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider2D))]
    public class ZoneObserver : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEnter;
        public event Action<Collider2D> TriggerStay;
        public event Action<Collider2D> TriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStay?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit?.Invoke(other);
        }
    }
}
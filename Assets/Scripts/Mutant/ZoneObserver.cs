using System;
using UnityEngine;

namespace Mutant
{
    [RequireComponent(typeof(Collider2D))]
    public class ZoneObserver : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEnter;
        public event Action<Collider2D> TriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit?.Invoke(other);
        }
    }
}
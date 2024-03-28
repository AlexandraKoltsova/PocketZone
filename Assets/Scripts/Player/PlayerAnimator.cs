using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("Run");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rb;

        private void Update()
        {
            _animator.SetFloat(MoveHash, _rb.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}
using DefaultNamespace;
using Services;
using Services.Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4f;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _mesh;
        
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void FixedUpdate()
        {
            Vector2 movementVector;
            
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _inputService.Axis;
                
                if (_inputService.Axis.x < Constants.Epsilon)
                {
                    ChangeDirection(-1);
                }
                else
                {
                    ChangeDirection(1);
                }
            }
            else
            {
                movementVector = Vector2.zero;
            }
            
            _rb.velocity = _movementSpeed * movementVector.normalized;
        }

        private void ChangeDirection(float x)
        {
            _mesh.transform.localScale = new Vector3(x, 1, 1);
        }
    }
}
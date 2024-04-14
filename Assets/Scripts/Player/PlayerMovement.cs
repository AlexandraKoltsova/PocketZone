using DefaultNamespace;
using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public int MoveSpeed;
        
        private Rigidbody2D _rb;
        private Collider2D _collider;
        private Transform _mesh;

        private IInputSystem _inputSystem;
        
        public void Init(IInputSystem inputSystem, Rigidbody2D rb, Collider2D collider, Transform mesh)
        {
            _inputSystem = inputSystem;
            _rb = rb;
            _collider = collider;
            _mesh = mesh;
        }
        
        public void FixedTick(bool canMove)
        {
            if (!canMove)
            {
                return;
            }
            
            Vector2 movementVector;
            
            if (_inputSystem.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _inputSystem.Axis;
                
                if (_inputSystem.Axis.x < Constants.Epsilon)
                    ChangeDirection(-1);
                else
                    ChangeDirection(1);
            }
            else
            {
                movementVector = Vector2.zero;
            }
            
            _rb.velocity = MoveSpeed * movementVector.normalized;
        }

        private void ChangeDirection(float x)
        {
            _mesh.transform.localScale = new Vector3(x, 1, 1);
        }

        /*public void SaveProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _collider.enabled = false;
            transform.position = to.AsUnityVector();
            _collider.enabled = true;
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }*/
    }
}
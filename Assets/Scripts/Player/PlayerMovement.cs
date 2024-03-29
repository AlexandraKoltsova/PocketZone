using DefaultNamespace;
using Data;
using Data.Position;
using Services;
using Services.Input;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float _movementSpeed = 4f;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Transform _mesh;
        
        private IInputService _inputService;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
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

        public void SaveProgress(PlayerProgress progress)
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
        }

        private void OnDisable()
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved.");
        }
    }
}
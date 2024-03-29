using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            var bootstriper = FindObjectOfType<GameBootstrapper>();

            if(bootstriper == null)
                Instantiate(_bootstrapperPrefab);
        }
    }
}
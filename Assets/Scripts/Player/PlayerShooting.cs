using Data;
using Data.PlayerStatus;
using Services.PersistentProgress;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private GameObject[] _bullets;
        
        private Stats _stats;

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.Stats;
        }

        private void Start()
        {
            _aim.OnShoot += OnShoot;
        }

        private void OnShoot(PlayerAim.OnShootEvent obj)
        {
            Debug.DrawLine(obj.GunEndPointPosition, obj.ShootPosition + Vector3.up, Color.white, 0.1f);
            
            int index = FindBullet();
            _bullets[index].transform.position = obj.GunEndPointPosition;

            Vector3 direction = ((obj.ShootPosition + Vector3.up) - obj.GunEndPointPosition).normalized;
            _bullets[index].GetComponent<Bullet>().SetDirection(direction, _stats.Damage, _stats.Speed);
        }

        private int FindBullet()
        {
            for (int i = 0; i < _bullets.Length; i++)
            {
                if (!_bullets[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }

        private void OnDisable()
        {
            _aim.OnShoot -= OnShoot;
        }
    }
}
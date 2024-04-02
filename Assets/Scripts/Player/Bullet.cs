using Data.PlayerStatus;
using Logic;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;
        private float _damage;
        private float _speed;
        private float _lifetime;

        private Stats _stats;

        private void Start()
        {
            gameObject.SetActive(false);
        }
        
        private void Update()
        {
            transform.position += _direction * _speed * Time.deltaTime;

            _lifetime += Time.deltaTime;
            if (_lifetime > 5)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.TryGetComponent(out IHealth mutantHealth))
            {
                Debug.Log(_damage);
                mutantHealth.TakeDamage(_damage);
                
                gameObject.SetActive(false);
            }
        }

        public void SetDirection(Vector3 direction, float damage, float speed)
        {
            gameObject.SetActive(true);
            _lifetime = 0;
            _direction = direction;
            _damage = damage;
            _speed = speed;

            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
        }

        private float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (n < 0)
            {
                n += 360;
            }

            return n;
        }
    }
}
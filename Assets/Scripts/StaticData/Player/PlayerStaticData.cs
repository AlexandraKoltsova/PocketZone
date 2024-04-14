using UnityEngine;

namespace StaticData.Player
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "StaticData/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        [Range(1, 100)]
        public int MaxHP;

        [Range(0, 50)]
        public int MoveSpeed = 3;

        [Range(1, 10)]
        public float AimRadius;

        [Range(1, 30)]
        public int Damage;

        [Range(1, 10)]
        public int BulletSpeed;
    }
}
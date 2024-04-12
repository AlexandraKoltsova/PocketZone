using UnityEngine;

namespace StaticData.Mutant
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "StaticData/Mutant")]
    public class MutantStaticData : ScriptableObject
    {
        public MutantTypeId MutantTypeId;

        [Range(1, 100)]
        public int Hp;

        [Range(1, 30)]
        public int Damage;
        
        [Range(1f, 10f)]
        public float AttackColldown;

        [Range(0.5f, 5f)]
        public float EffectiveDistance;

        [Range(0.5f, 5f)]
        public float CLeavage;

        [Range(0, 10)]
        public int MoveSpeed = 3;

        public GameObject Prefab;
    }
}
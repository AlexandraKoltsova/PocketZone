using System.Collections.Generic;
using StaticData.MutantsData;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;

        public List<MutantSpawnerData> MutantSpawners;
    }
}
using System;
using UnityEngine;

namespace StaticData.Mutant
{
    [Serializable]
    public class MutantSpawnerData
    {
        public string Id;
        public MutantTypeId MutantTypeId;
        public Vector3 Position;
        
        public MutantSpawnerData(string id, MutantTypeId mutantTypeId, Vector3 position)
        {
            Id = id;
            MutantTypeId = mutantTypeId;
            Position = position;
        }
    }
}
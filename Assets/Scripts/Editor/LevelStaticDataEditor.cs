using System.Linq;
using Logic;
using Logic.Spawners;
using StaticData;
using StaticData.MutantsData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelData = (LevelStaticData)target;
            
            if (GUILayout.Button("Collect"))
            {
                levelData.MutantSpawners = FindObjectsOfType<SpawnerMarker>()
                        .Select(x => new MutantSpawnerData(x.GetComponent<UniqueId>().Id, x.MutantTypeId, x.transform.position))
                        .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}
using Logic.Spawners;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MutantSpawner))]
    public class SpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(MutantSpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.yellow;
            
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}
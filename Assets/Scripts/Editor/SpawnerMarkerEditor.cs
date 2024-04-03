using Logic.Spawners;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SpawnerMarker))]
    public class SpawnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnerMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.yellow;
            
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}
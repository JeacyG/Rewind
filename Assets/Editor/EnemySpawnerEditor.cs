using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemySpawner spawner = (EnemySpawner)target;
        
        GUILayout.Space(10);

        if (GUILayout.Button("Spawn Enemy"))
        {
            if (Application.isPlaying)
            {
                spawner.SpawnOne();
            }
        }
        
        if (GUILayout.Button("Stop Spawning"))
        {
            if (Application.isPlaying)
            {
                spawner.StopSpawning();
            }
        }

        if (GUILayout.Button("Kill All"))
        {
            if (Application.isPlaying)
            {
                spawner.KillAll();
            }
        }
    }
}

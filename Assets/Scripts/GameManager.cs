using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CloneManager cloneManager;
    [SerializeField] private Transform spawnPoint;

    public void Rewind(List<ActionData> actions)
    {
        cloneManager.CreateNewClone(actions, spawnPoint);
        cloneManager.ResetAllClones();
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}

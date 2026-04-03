using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    private List<Transform> players = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(Transform player)
    {
        if (!players.Contains(player))
            players.Add(player);
    }

    public void UnregisterPlayer(Transform player)
    {
        players.Remove(player);
    }

    public Transform GetClosestPlayer(Vector2 position)
    {
        Transform closest = null;
        float minDistSqr = float.MaxValue;

        foreach (Transform player in players)
        {
            if (player.IsUnityNull())
                continue;
            
            float distSqr = ((Vector2)player.position - position).sqrMagnitude;

            if (distSqr < minDistSqr)
            {
                minDistSqr = distSqr;
                closest = player;
            }
        }
        
        return closest;
    }
}

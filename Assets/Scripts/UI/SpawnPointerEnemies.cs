using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointerEnemies : MonoBehaviour
{
    public Transform[] spawnLocations;
    public Transform playerTransform;
    public GameObject arrow;

    void Update()
    {
        // Find the closest spawn location to the player
        Transform closestSpawn = null;
        float closestDistance = float.MaxValue;

        foreach (Transform spawn in spawnLocations)
        {
            float distance = Vector3.Distance(spawn.position, playerTransform.position);

            if (distance < closestDistance)
            {
                closestSpawn = spawn;
                closestDistance = distance;
            }
        }

        // Set the arrow position and rotation to point towards the closest spawn location
        arrow.transform.position = closestSpawn.position;
        arrow.transform.LookAt(playerTransform.position);
    }

    private Transform GetClosestSpawn()
    {
        Transform closestSpawn = null;
        float closestDistance = float.MaxValue;

        // Find the closest spawn location to the player
        foreach (Transform spawn in spawnLocations)
        {
            float distance = Vector3.Distance(spawn.position, transform.position);
            if (distance < closestDistance)
            {
                closestSpawn = spawn;
                closestDistance = distance;
            }
        }

        return closestSpawn;
    }
}

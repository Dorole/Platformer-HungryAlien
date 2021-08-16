using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager instance;

    public List<Transform> spawnPoints;
    public GameObject fallingPlatform;
    public float waitBeforeSpawning = 5.0f;
    public GameObject instantiatedPlatform;

    private void Awake()
    {
        instance = this;
        foreach (Transform spawnPoint in spawnPoints)
        {
            instantiatedPlatform = Instantiate(fallingPlatform, spawnPoint.position, fallingPlatform.transform.rotation);
        }

    }

    IEnumerator SpawnPlatform (Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(waitBeforeSpawning);

        instantiatedPlatform = Instantiate(fallingPlatform, spawnPosition, fallingPlatform.transform.rotation);
    }
}

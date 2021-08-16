using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformToSpawn;

    public Transform spawnPoint;

    public float spawnTime = 2.0f;
    public float repeatRate = 2.0f;

    private void Start()
    {
        InvokeRepeating("SpawnPlatform", spawnTime, repeatRate);
    }

    private void SpawnPlatform()
    {
        Instantiate(platformToSpawn, spawnPoint.position, Quaternion.identity);        
    }






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnPoint : MonoBehaviour
{
    private bool _isEmpty;
    private GameObject _platform;

    private void Start()
    {
        _platform = PlatformManager.instance.instantiatedPlatform;
        _isEmpty = _platform.GetComponent<FallingPlatform>().isFallen;
    }

    private void Update()
    {
        if (_isEmpty)
        {
            PlatformManager.instance.StartCoroutine("SpawnPlatform", transform.position);

        }
    }
}




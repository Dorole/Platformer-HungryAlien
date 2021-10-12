using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject gunPrefab;

    public float waitBeforeSpawning = 5.0f;
    public float timer;

    private void Start()
    {
        GameEvents.instance.onGunExpired += SpawnGun;
    }

    private void SpawnGun()
    {
        if (transform.childCount == 0)
            StartCoroutine(TimedGunSpawn());
    }

    private IEnumerator TimedGunSpawn ()
    {
        yield return new WaitForSeconds(waitBeforeSpawning);

        GameObject newGun = Instantiate(gunPrefab, transform.position, Quaternion.identity);
        newGun.name = "LaserGun";
        newGun.transform.parent = transform;

        Transform firingPoint = newGun.transform.Find("FiringPoint");
        newGun.GetComponent<Shoot>().firingPointTransform = firingPoint;

    }
 }

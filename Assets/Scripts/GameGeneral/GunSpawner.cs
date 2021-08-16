using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public bool isEmpty = false;
    public bool playerHasGun = false;

    public GameObject gunPrefab;

    public float waitBeforeSpawning = 5.0f;
    public float timer;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isEmpty && !playerHasGun && (Time.time >= timer))
        {
            GameObject newGun = Instantiate(gunPrefab, transform.position, Quaternion.identity);
            newGun.name = "LaserGun";
            newGun.transform.parent = transform;

            Transform firingPoint = newGun.transform.Find("FiringPoint");
            newGun.GetComponent<Shoot>().firingPointTransform = firingPoint;

            isEmpty = false;
        }

    }

 }

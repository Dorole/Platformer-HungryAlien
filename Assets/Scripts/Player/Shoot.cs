using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject laserPrefab;
	public Transform firingPointTransform;

	public float firingPower = 50.0f;

    public bool canFire = false;
	private bool _shouldFire = false;

    private void Start()
    {
		if (firingPointTransform == null)
			firingPointTransform = transform.Find("FiringPoint");
    }

    private void Update()
    {
        _shouldFire = false;

		if (Input.GetButtonDown("Fire1"))
		{
			if (!canFire)
				return;

			_shouldFire = true;
			//FindObjectOfType<AudioManager>().Play("Shoot");
		}

		if (_shouldFire)
		{
			if (laserPrefab == null)
				return;

			GameObject newLaser = Instantiate(laserPrefab, firingPointTransform.position, Quaternion.identity);

		}
	}
}

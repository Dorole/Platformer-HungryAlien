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
		}

		if (_shouldFire)
		{
			if (laserPrefab == null)
				return;

			AudioManager.instance.Play("Shoot");
			Instantiate(laserPrefab, firingPointTransform.position, Quaternion.identity);
		}
	}
}

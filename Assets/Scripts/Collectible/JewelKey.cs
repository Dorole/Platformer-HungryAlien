using UnityEngine;

public class JewelKey : MonoBehaviour
{
	public GameObject jewelLock;
    private JewelLock _jewelLockScript;

    private void Awake()
    {
        _jewelLockScript = jewelLock.GetComponent<JewelLock>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag != "Player")
			return;

        _jewelLockScript.hasKey = true;

        AudioManager.instance.Play("Gem");
        Destroy(gameObject);
	}
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float StunDuration;
    public float waitBeforeChangingLayers = 2.0f;

    private bool _isStunned = false;
    private float _timer = 0.0f;

    /*private void ChangeLayer(string layerName)
    {
        int newLayer = LayerMask.NameToLayer(layerName);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = newLayer;
        }
    }*/

    private void Update()
    {
        if (Time.time >= _timer)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");

            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
            else
                return;
        }
    }

    public void Stun()
    {
        if (_isStunned)
            return;

        _isStunned = true;
        FindObjectOfType<AudioManager>().Play("EnemyStun");
        GetComponentInParent<EnemyMover>().Stun();

        GetComponentInChildren<StunChecker>().JumpCount = 1;

        gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        //transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NonInteractable");

        //ChangeLayer("NonInteractable");

        Invoke("DisableStun", StunDuration);
    }

    private void DisableStun()
    {
        //ChangeLayer("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
        _isStunned = false;

        GetComponentInChildren<StunChecker>().JumpCount = 0;

        GetComponentInParent<EnemyMover>().Stun(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile") 
        {
            Killshot();
            Destroy(collision.gameObject);
        }

        if (_isStunned == true)
            return;

        if (collision.gameObject.tag != "Player")
            return;

        HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

        if (otherHealthManager == null)
            return;

        gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NonInteractable");

        _timer = Time.time + waitBeforeChangingLayers;

        otherHealthManager.TakeDamage();

    }

    public void Die()
    {
        if (_isStunned == false)
            return;

        AudioManager.instance.Play("EnemyDeath");
        GetComponentInParent<EnemyMover>().Die();
        GetComponentInParent<EnemyMover>().enabled = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Destroy(gameObject, 1.0f);
    }

    public void Killshot()
    {
        AudioManager.instance.Play("EnemyDeath");
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInParent<EnemyMover>().Die();
        GetComponentInParent<EnemyMover>().enabled = false;
        Destroy(gameObject, 1.0f);
    }
}

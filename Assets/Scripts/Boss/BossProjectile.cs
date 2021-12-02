using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private GameObject _target;
    private BossHealth _bossHealth;
    public float speed = 20.0f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

        Physics2D.IgnoreCollision(GameObject.Find("Platform").GetComponent<Collider2D>(), gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreLayerCollision(18, 17);

        AudioManager.instance.Play("BossShoot");

        Vector2 moveDirection = (_target.transform.position - transform.position).normalized * speed;
        _rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_bossHealth.health <= 0)
                Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<CircleCollider2D>());

            HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

            if (otherHealthManager == null)
                return;

            otherHealthManager.TakeDamage();
            Destroy(gameObject);
        }
    }
}

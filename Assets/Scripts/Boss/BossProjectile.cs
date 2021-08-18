using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private GameObject _target;
    public float speed = 20.0f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreCollision(GameObject.Find("Platform").GetComponent<Collider2D>(), gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreLayerCollision(18, 17);

        FindObjectOfType<AudioManager>().Play("BossShoot");

        Vector2 moveDirection = (_target.transform.position - transform.position).normalized * speed;
        _rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        Destroy(gameObject, 0.7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

            if (otherHealthManager == null)
                return;

            otherHealthManager.TakeDamage();
            Destroy(gameObject);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChecker : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public int projectileDamage = 10;
    public int jumpDamage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            GetComponentInParent<BossHealth>().TakeDamage(jumpDamage);
        }

        else if (collision.gameObject.tag == "Projectile")
            GetComponentInParent<BossHealth>().TakeDamage(projectileDamage);

        else
            return;
    }
}

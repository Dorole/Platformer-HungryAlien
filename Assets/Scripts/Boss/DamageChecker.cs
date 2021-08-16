using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChecker : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public int projectileDamage = 10;
    public int jumpDamage = 5;

    private BossHealth boss;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GetComponentInParent<BossHealth>().TakeDamage(projectileDamage);
        }

        else if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<BossHealth>().TakeDamage(jumpDamage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }

        else
            return;
    }
}

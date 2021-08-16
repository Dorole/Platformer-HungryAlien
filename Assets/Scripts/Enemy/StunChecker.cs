using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunChecker : MonoBehaviour
{
    public float BounceForce = 10.0f;

    public int JumpCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GetComponentInParent<EnemyController>().Killshot();
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Player")
        {

            if (JumpCount == 1)
            {
                GetComponentInParent<EnemyController>().Die();
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
            }

            if (JumpCount == 0)
            {
                GetComponentInParent<EnemyController>().Stun();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
            }

            //stun sfx
        }

        else
            return;


    }

}

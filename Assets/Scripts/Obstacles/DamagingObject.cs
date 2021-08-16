using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    public float waitAfterPlayerDeath = 1.0f;
   
    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;


        if (gameObject.tag == "Chaser")
        {
            Physics2D.IgnoreCollision(GameObject.Find("Platform").GetComponent<Collider2D>(), gameObject.GetComponent<CircleCollider2D>());
            GameManager.instance.isChaserActive = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "Player")
            return;

        HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

        if (otherHealthManager == null)
            return;

        if ((gameObject.tag == "InstantDeath") || (gameObject.tag == "Chaser"))
            otherHealthManager.Die();
        else
            otherHealthManager.TakeDamage();
    }

    public void ResetPosition()
    {
        transform.position = _startingPosition;
    }

}

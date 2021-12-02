using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
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
        if (!collision.gameObject.CompareTag("Player"))
            return;

        HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

        if (otherHealthManager == null)
            return;

        if ((gameObject.tag == "InstantDeath") || (gameObject.tag == "Chaser"))
        {
            if(GameManager.instance.bossLevel)
                GameManager.instance.isPlayerDead = true;

            otherHealthManager.Die();
        }   
        else
            otherHealthManager.TakeDamage();
    }

    public void ResetPosition()
    {
        //instantiate u game manageru ako je kugla null
        transform.position = _startingPosition;
    }

}

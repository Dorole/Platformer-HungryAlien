using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool isFlipped = false;

    public GameObject bossProjectilePrefab;
    public Transform bossFiringPoint;
    public float firingPower = 50.0f;

    public int projectileDamage = 10;

    public Transform player;

    private int _bossHealth;

    public float startingFireRate = 1.5f;
    public float damagedFireRate = 1.0f;
    public float dyingFireRate = 0.3f;
    private float _fireRate;

    private float _nextTimeToFire;
    private float _nextTimeToSearch = 2.0f;
 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _bossHealth = GetComponent<BossHealth>().health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GetComponentInParent<BossHealth>().TakeDamage(projectileDamage);
        }

        if (collision.gameObject.tag == "Player")
        {
            HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

            if (otherHealthManager == null)
                return;

            otherHealthManager.TakeDamage();
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1.0f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void AttackPlayer()
    {
        if (bossProjectilePrefab == null)
            return;

        if (_bossHealth > 60)
            _fireRate = startingFireRate;
        else if (_bossHealth <= 60 && _bossHealth > 30)
            _fireRate = damagedFireRate;
        else if (_bossHealth <= 30)
            _fireRate = dyingFireRate;
        
        if (_nextTimeToFire < Time.time)
        {
            Instantiate(bossProjectilePrefab, bossFiringPoint.position, Quaternion.identity);
            _nextTimeToFire = Time.time + _fireRate;
        }
    }

    public void FindPlayer()
    {
        if (_nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                player = searchResult.transform;
            _nextTimeToSearch = Time.time + 2.0f;
        }

    }
}

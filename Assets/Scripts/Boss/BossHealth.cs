using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth;
    public float nextTimeToHit = 2.0f;

    private bool _isInvulnerable = false;
    [SerializeField] private GameObject _bossDeathParticleSystem;

    private void Start()
    {
        maxHealth = health;
    }

    public void TakeDamage (int damage)
    {
        if (_isInvulnerable)
            return;

        health -= damage;

        StartCoroutine(InvulnerableBoss());
            
        if (health <= 0)
            Die();
    }

    void Die()
    {
        //GameManager.instance.StartCoroutine(GameManager.instance.EndLevel());
        if (_bossDeathParticleSystem != null)
        {
            GameObject bossParticles = Instantiate(_bossDeathParticleSystem, transform.position, Quaternion.identity);
            Destroy(bossParticles, 3.0f);
        }

        Destroy(gameObject);
    }

    IEnumerator InvulnerableBoss()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(nextTimeToHit);

        _isInvulnerable = false;
    }

}

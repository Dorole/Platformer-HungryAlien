using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth;
    public float nextTimeToHit = 2.0f;

    private bool _isInvulnerable = false;
    private Animator _bossAnimator;

    private void Start()
    {
        maxHealth = health;
        _bossAnimator = GetComponent<Animator>();
    }

    public void TakeDamage (int damage)
    {
        if (_isInvulnerable)
            return;

        health -= damage;

        FindObjectOfType<AudioManager>().Play("BossHit");
        _bossAnimator.SetBool("isHurt", true);

        if (health <= 0)
            GameManager.instance.StartCoroutine(GameManager.instance.EndLevel()); 
        else
            StartCoroutine(InvulnerableBoss());
            
    }

    IEnumerator InvulnerableBoss()
    {
         _isInvulnerable = true;

        yield return new WaitForSeconds(0.35f);
        _bossAnimator.SetBool("isHurt", false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(nextTimeToHit);
        _isInvulnerable = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

}

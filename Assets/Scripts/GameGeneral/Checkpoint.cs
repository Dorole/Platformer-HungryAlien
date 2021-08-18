using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        
        if (_animator != null)
            _animator.SetBool("IsVictory", true);

        FindObjectOfType<AudioManager>().Play("Checkpoint");
        GameManager.instance.spawnPoint = gameObject;

        gameObject.GetComponent<Collider2D>().enabled = false;

    }

}

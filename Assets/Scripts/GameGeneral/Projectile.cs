using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody2D _rb;
    private GameObject _player;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Start()
    {

        if (_player.transform.localScale.x > 0)
            _rb.velocity = transform.right * speed;
        else
            _rb.velocity = transform.right * speed * -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float WaitBeforeAnimationExit = 0.5f;
    public float BounceForce = 10.0f;
    private Animator _animator;

    private float _timer = 0.0f;

    private void Awake()
    {   
        if (gameObject.tag != "BouncyCandy")
            _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameObject.tag != "BouncyCandy")
        {
            if (Time.time >= _timer)
                _animator.SetBool("IsPlayerOn", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        //bounce = true;

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);

        if (gameObject.tag != "BouncyCandy")
        {
            _animator.SetBool("IsPlayerOn", true);
            _timer = Time.time + WaitBeforeAnimationExit;
        }


    }
   
}

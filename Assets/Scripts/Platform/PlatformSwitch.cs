using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
    public GameObject platform;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
            return;

        _animator.SetBool("isHitWithProjectile", true);
        platform.GetComponentInParent<PlatformMover>().shouldMove = true;

    }
}

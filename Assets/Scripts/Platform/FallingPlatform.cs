using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D _platformRB;
    public float fallDelay = 1.0f;
    public float waitAfterFalling = 5.0f;
    public bool isFallen;
    public Vector3 platformPosition;

    private void Awake()
    {
        _platformRB = GetComponent<Rigidbody2D>();
        platformPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        GameManager.instance.StartCoroutine("SpawnPlatform", platformPosition);
        StartCoroutine(Fall());
        Destroy(gameObject, 5.0f);

    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        _platformRB.isKinematic = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        GetComponent<BoxCollider2D>().isTrigger = false;
        GameManager.instance.StartCoroutine(GameManager.instance.EndLevel());
    }
}

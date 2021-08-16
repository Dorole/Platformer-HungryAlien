using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private GameObject _parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chaser")
        {
            _parent = collision.transform.parent.gameObject;

            GameManager.instance.isChaserActive = false;
            Destroy(_parent);
        }
        else
            return;
    }
}

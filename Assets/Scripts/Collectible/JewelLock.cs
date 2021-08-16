using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelLock : MonoBehaviour
{
    public Transform objectToInstantiate;
    public Transform chest;

    public bool hasKey;
    private Animator _chestAnimator;

    
    private void Awake ()
    {
        hasKey = false;

        _chestAnimator = chest.GetComponent<Animator>();
        _chestAnimator.SetBool("shouldOpen", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (!hasKey)
            return;

        UnlockJewel();
    }

    private void UnlockJewel()
    {
        _chestAnimator.SetBool("shouldOpen", true);

        objectToInstantiate = Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
        objectToInstantiate.transform.SetParent(null);

        Destroy(gameObject);
    }

}

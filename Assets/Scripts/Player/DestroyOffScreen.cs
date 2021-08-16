using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        if (!GameManager.instance.bossLevel)
            return;

        while (GameManager.instance.isChaserActive)
            gameObject.GetComponent<HealthManager>().Die();
    }
}

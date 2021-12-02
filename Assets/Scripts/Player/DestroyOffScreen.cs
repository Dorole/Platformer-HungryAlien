using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        if (!GameManager.instance.bossLevel)
            return;

        while (GameManager.instance.isChaserActive)
        {
            if (GameManager.instance.isPlayerDead)
                return;

            gameObject.GetComponent<HealthManager>().Die();
        }
            
    }
}

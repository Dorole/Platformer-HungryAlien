using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.instance.StartCoroutine(GameManager.instance.EndLevel());
    }
}

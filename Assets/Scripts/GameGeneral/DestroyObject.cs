using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private GameObject _parent;
    [SerializeField] private GameObject _snowballParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chaser")
        {
            _parent = collision.transform.parent.gameObject;

            GameManager.instance.isChaserActive = false;
                 
            Destroy(_parent);
            AudioManager.instance.Play("SnowballDestroyed");
            GameObject snowParticles = Instantiate(_snowballParticles, transform.position, Quaternion.identity);

            AudioManager.instance.Stop("Snowball");

            Destroy(snowParticles, 3f);
        }
        else
            return;
    }
}

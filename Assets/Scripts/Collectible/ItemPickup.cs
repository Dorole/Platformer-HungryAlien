using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (gameObject.tag == "Life")
        {
            transform.parent = collision.transform;
            transform.position = new Vector3(collision.transform.position.x + _offset.x, collision.transform.position.y + _offset.y, collision.transform.position.z);

            HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

            if (otherHealthManager == null)
                return;

            otherHealthManager.pickedUpLife = true;
            otherHealthManager.health++;

            FindObjectOfType<AudioManager>().Play("Life");
        } 
        
        else if (gameObject.tag == "LaserGun")
        {
            FindObjectOfType<AudioManager>().Play("GunAcquired");
            transform.parent = collision.transform;

            bool _isPlayerFacingRight = collision.GetComponent<PlayerMovementController>()._isFacingRight;
            Vector3 localScale = transform.localScale;

            if (_isPlayerFacingRight)
                transform.position = new Vector3(collision.transform.position.x + _offset.x, collision.transform.position.y + _offset.y, collision.transform.position.z);

            else
            {
                localScale.x *= -1.0f;
                transform.localScale = localScale;
                transform.position = new Vector3(collision.transform.position.x + (_offset.x * -1.0f), collision.transform.position.y + _offset.y, collision.transform.position.z);
            }

            gameObject.GetComponent<Shoot>().canFire = true;
            GameManager.instance.UpdateLaserBar();
        } 
        
        else
        {
            //play audio
            Destroy(gameObject);
        }
    }
}

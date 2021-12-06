using UnityEngine;

public class Boss : MonoBehaviour
{
    private bool _isFlipped = false;

    public GameObject bossProjectilePrefab;
    public Transform bossFiringPoint;
    public Canvas healthCanvas;
    public float firingPower = 50.0f;

    public int projectileDamage = 10;

    public Transform player;

    private BossHealth _bossHealth;

    public float startingFireRate = 1.5f;
    public float damagedFireRate = 1.0f;
    public float dyingFireRate = 0.3f;
    private float _fireRate;

    private float _nextTimeToFire;
    private float _nextTimeToSearch = 2.0f;

    //private Vector3 _flippedCanvasPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _bossHealth = GetComponent<BossHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //FEEDBACK: koristiti reference umjesto get component i imat provjeru za null
            GetComponentInParent<BossHealth>().TakeDamage(projectileDamage);
        }

        if (collision.gameObject.tag == "Player")
        {
            HealthManager otherHealthManager = collision.gameObject.GetComponent<HealthManager>();

            if (otherHealthManager == null)
                return;

            otherHealthManager.TakeDamage();
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1.0f;

        //FEEDBACK: odvojiti recimo ovo u zasebnu dijecu jednog roditelja i onda samo
        // okretat sprite bossa a canvas ostavit istim
        Vector3 canvasFlipped = healthCanvas.transform.localScale;

        if (transform.position.x > player.position.x && _isFlipped)
        {
            transform.localScale = flipped;
            canvasFlipped.x *= -1;
            healthCanvas.transform.localScale = canvasFlipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !_isFlipped)
        {
            transform.localScale = flipped;
            canvasFlipped.x *= -1;
            healthCanvas.transform.localScale = canvasFlipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = true;
        }
    }

    public void AttackPlayer()
    {
        if (bossProjectilePrefab == null)
            return;

        //FEEDBACK: izbjegavati hardkodirane brojke, prebacit u varijable ili slozit kao
        // listu health i firerate kombinacija
        if (_bossHealth.health > 60)
            _fireRate = startingFireRate;
        else if (_bossHealth.health <= 60 && _bossHealth.health > 30)
            _fireRate = damagedFireRate;
        else if (_bossHealth.health <= 30)
            _fireRate = dyingFireRate;

        if (_nextTimeToFire < Time.time)
        {
            Instantiate(bossProjectilePrefab, bossFiringPoint.position, Quaternion.identity);
            _nextTimeToFire = Time.time + _fireRate;
        }
    }

    public void FindPlayer()
    {
        if (_nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                player = searchResult.transform;
            _nextTimeToSearch = Time.time + 2.0f;
        }

    }

}

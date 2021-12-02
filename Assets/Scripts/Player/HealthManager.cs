using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Vector3 startingPosition;
    public Slider appleSlider;
    public float waitBeforeRespawningPlayer = 2.0f;
    public int health;

    [HideInInspector] public bool shouldSpawnLife;
    [HideInInspector] public bool hasExtraLife;
    [SerializeField] private GameObject _lifePrefab;
    private Vector3 _lifePos;

    private TriggerBoss _trigger;
    [SerializeField] private ParticleSystem _lifeParticleSystem;

    private void Start()
    {
        if (appleSlider == null)
            appleSlider = GameObject.FindGameObjectWithTag("AppleBar").GetComponent<Slider>();

        startingPosition = transform.position;

        health = 1;

        if (GameManager.instance.bossLevel)
        {
            _lifePos = GameObject.FindGameObjectWithTag("Life").transform.position;
            _trigger = GameObject.FindGameObjectWithTag("BossTrigger").GetComponent<TriggerBoss>();
        }
     }

    private void Update()
    {
        Starvation();
    }

    public void Die()
    {
        if(AppleBarSlider.instance != null)
            AppleBarSlider.instance.StopAllCoroutines();
        
        GameManager.instance.StartCoroutine(GameManager.instance.RespawnPlayer());

        if (GameManager.instance.laserSlider != null && GameManager.instance.laserSlider.IsActive())
        {
            LaserBarSlider.instance.StopAllCoroutines();
            GameManager.instance.laserCanvas.SetActive(false);
        }

        if (GameManager.instance.gunAvailable)
        {
            GameObject[] gunSpawners;
            gunSpawners = GameObject.FindGameObjectsWithTag("GunSpawner");

            foreach (GameObject gunSpawner in gunSpawners)
            {
                if (gunSpawner.transform.childCount == 0)
                    GameEvents.instance.SpawnGun();
            }
        }

        if(GameManager.instance.bossLevel)
        {
            if (_trigger.isTriggered)
                _trigger.UndoTrigger();

            if (shouldSpawnLife)
            {
                GameObject extraLife = Instantiate(_lifePrefab, _lifePos, Quaternion.identity);
                extraLife.name = "Life";
                extraLife.transform.parent = GameObject.FindGameObjectWithTag("LifeParentObject").transform;
            }
        }

        GameManager.instance.isPlayerDead = true;

        AudioManager.instance.Play("PlayerDeath");
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        if (hasExtraLife)
        {
            GameObject life = transform.Find("Life").gameObject;

            if (_lifeParticleSystem != null)
            {
                ParticleSystem lifeParticleSystem = Instantiate(_lifeParticleSystem, life.transform.position, Quaternion.identity);
                lifeParticleSystem.transform.SetParent(null);
                lifeParticleSystem.Play();
            }

           hasExtraLife = false;

           AudioManager.instance.Play("LifeDestroyed");
           Destroy(life);

        }

        health--;

        if (health == 0)
            Die();
    }

    private void Starvation()
    {
        if (appleSlider.value <= 0)
        {
            Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Vector3 startingPosition;

    public Slider appleSlider;

    public float waitBeforeRespawningPlayer = 2.0f;

    public int health;

    public GameObject extraLife;
    public bool pickedUpLife;

    private GameManager _gm;

    private int _maxSliderValue;
    private Animator _animator;
    private TriggerBoss _trigger;

    [SerializeField] private ParticleSystem _lifeParticleSystem;

    private void Awake()
    {
        _gm = GameManager.instance;
    }

    private void Start()
    {
        if (appleSlider == null)
            appleSlider = GameObject.FindGameObjectWithTag("AppleBar").GetComponent<Slider>();

        _maxSliderValue = _gm.maxSliderValue;

        startingPosition = transform.position;
        _animator = GetComponent<Animator>();

        extraLife = GameObject.FindGameObjectWithTag("Life");

        health = 1;
        pickedUpLife = false;

        if(_gm.bossLevel)
            _trigger = GameObject.Find("BossTrigger").GetComponent<TriggerBoss>();
    }

    private void Update()
    {
        Starvation();

    }

    public void Die()
    {
        if(AppleBarSlider.instance != null)
            AppleBarSlider.instance.StopAllCoroutines();
        
        GetComponent<PlayerMovementController>().enabled = false;
        _animator.SetBool("IsHurt", true);

        _gm.StartCoroutine(_gm.RespawnPlayer());

        if (_gm.laserSlider != null && _gm.laserSlider.IsActive())
        {
            LaserBarSlider.instance.StopAllCoroutines();
            _gm.laserCanvas.SetActive(false);
        }

        if (_gm.gunAvailable)
        {
            GameObject[] gunSpawners;
            gunSpawners = GameObject.FindGameObjectsWithTag("GunSpawner");

            foreach (GameObject gunSpawner in gunSpawners)
            {
                bool playerHasGun = gunSpawner.GetComponent<GunSpawner>().playerHasGun;

                if (playerHasGun)
                    playerHasGun = false;
            }
        }

        if(_gm.bossLevel)
        {
            if (_trigger.isTriggered)
                _trigger.UndoTrigger();
        }

        Destroy(gameObject);

    }

    public void TakeDamage()
    {
        if (pickedUpLife)
        {
            if (extraLife == null)
                return;

            if (_lifeParticleSystem != null)
            {
                ParticleSystem lifeParticleSystem = Instantiate(_lifeParticleSystem, extraLife.transform.position, Quaternion.identity);
                lifeParticleSystem.transform.SetParent(null);
                lifeParticleSystem.Play();
            }

            Destroy(extraLife);
            pickedUpLife = false;

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

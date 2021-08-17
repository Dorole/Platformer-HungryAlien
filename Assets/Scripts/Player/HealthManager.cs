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

    //private float _maxSliderValue;
    private Animator _animator;
    private TriggerBoss _trigger;

    [SerializeField] private ParticleSystem _lifeParticleSystem;

    private void Start()
    {
        if (appleSlider == null)
            appleSlider = GameObject.FindGameObjectWithTag("AppleBar").GetComponent<Slider>();

        //_maxSliderValue = appleSlider.maxValue;
        //_maxSliderValue = _gm.maxSliderValue;

        startingPosition = transform.position;
        _animator = GetComponent<Animator>();

        extraLife = GameObject.FindGameObjectWithTag("Life");

        health = 1;
        pickedUpLife = false;

        if (GameManager.instance.bossLevel)
            _trigger = GameObject.FindGameObjectWithTag("BossTrigger").GetComponent<TriggerBoss>();
     }

    private void Update()
    {
        Starvation();

    }

    public void Die()
    {
        if(AppleBarSlider.instance != null)
            AppleBarSlider.instance.StopAllCoroutines();
        
        _animator.SetBool("IsHurt", true);

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
                //iz nekog razloga ne funkcionira s bool hasGun = gunSpawner.GetComponent<GunSpawner>().playerHasGun
                if (!gunSpawner.GetComponent<GunSpawner>().playerHasGun)
                    break;

                gunSpawner.GetComponent<GunSpawner>().playerHasGun = false;
            }
        }

        if(GameManager.instance.bossLevel)
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

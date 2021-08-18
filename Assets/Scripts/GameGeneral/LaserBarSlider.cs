using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBarSlider : MonoBehaviour
{
    public static LaserBarSlider instance;
    public Coroutine decreaseSlider;
    
    private Slider _laserSlider;
    public int currentSliderValue = 25;
    private WaitForSeconds _decreaseTick = new WaitForSeconds(1.0f);

    public GunSpawner _gunSpawner;
    
    [SerializeField] private ParticleSystem _laserParticleSystem;

    private void Awake()
    {
        instance = this;

        _laserSlider = GetComponent<Slider>();
        _laserSlider.value = currentSliderValue;
        _laserSlider.maxValue = currentSliderValue;

        if (GameObject.FindGameObjectWithTag("GunSpawner") != null)
            _gunSpawner = GameObject.FindGameObjectWithTag("GunSpawner").GetComponent<GunSpawner>();
    }

    public void DecreaseLaserSlider()
    {
        if (decreaseSlider != null)
            StopCoroutine(decreaseSlider);

        decreaseSlider = StartCoroutine(DecreaseSliderLaser(_laserSlider));
    }

    public IEnumerator DecreaseSliderLaser(Slider slider)
    {
        while (slider.value >= 0)
        {
            slider.value -= 1;

            yield return _decreaseTick;

            if (slider.value == 0) 
            {
                GameObject.FindGameObjectWithTag("LaserGun").GetComponent<Shoot>().canFire = false;
                _gunSpawner.playerHasGun = false;
                _gunSpawner.timer = Time.time + _gunSpawner.waitBeforeSpawning;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Destroy(player.transform.Find("LaserGun").gameObject);

                FindObjectOfType<AudioManager>().Play("LifeDestroyed");

                if (_laserParticleSystem != null)
                {
                    ParticleSystem laserParticleSystem = Instantiate(_laserParticleSystem, player.transform.position, Quaternion.identity);
                    laserParticleSystem.transform.SetParent(null);
                    laserParticleSystem.Play();
                }

                GameManager.instance.laserCanvas.SetActive(false);

                break;
            }
        }
        decreaseSlider = null;
    }
}

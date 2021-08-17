using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Start Level")]
    public bool bossLevel;
    private PlayerMovementController _playerMC;
    public bool gunAvailable;

    [Header("Spawning")]
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public float spawnDelay = 1.0f;
    public GameObject spawnParticlePrefab;
    public bool isChaserActive;
    public bool isPlayerDead;
    private GameObject _chaser;


    [Header("Slider")]
    public Slider appleSlider;
    public int maxSliderValue = 20;
    public GameObject laserCanvas;
    public Slider laserSlider;
    private GameObject _player;

    [Header("PlatformManager")]
    public GameObject fallingPlatform;
    public float waitBeforeSpawning = 5.0f;

    [Header("AppleSpawner")]
    public GameObject applePrefab;
    public float waitBeforeSpawningApple = 7.0f;

    [Header("End Level")]
    public GameObject friendToSave;
    public GameObject endParticlePrefab;
    public GameObject endCanvas;
    public Text floatingTextPrefab;
    public Button saveButton;
    public bool isLevelFinished;
    public float endDelay = 2.0f;
    private Animator _friendAnimator;
    private int _sceneIndex;

    private void Awake()
    {
        instance = this;
        _playerMC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_sceneIndex != 4)
        {
            if (_sceneIndex >= 2)
            {
                _playerMC.isDoubleJumpEnabled = true;
                gunAvailable = true;
            }

            if (_sceneIndex >= 3)
                _playerMC.isWallJumpEnabled = true;
        }

        if (_sceneIndex == 4)
        {
            bossLevel = true;
            _chaser = GameObject.FindGameObjectWithTag("Chaser");
            isChaserActive = true;
            gunAvailable = true;
        }

        if (laserCanvas == null)
            return;
        else
            laserCanvas.SetActive(false);

        if (friendToSave == null)
            return;
        else
            _friendAnimator = friendToSave.GetComponent<Animator>();

    }

    private void Start()
    {

    }

    public void UpdateAppleBar(int value)
    {

        if (appleSlider.value == 0)
            return;

        AppleBarSlider.instance.DecreaseAppleSlider();

        if (appleSlider.value == maxSliderValue)
            return;

        appleSlider.value += value;

    }

    public void UpdateLaserBar()
    {
        laserSlider.value = LaserBarSlider.instance.currentSliderValue;
        laserCanvas.SetActive(true);
        LaserBarSlider.instance.DecreaseLaserSlider();

    }

    public IEnumerator RespawnPlayer()
    {
        //audio manager or check brackeys 13. respawn effect
        yield return new WaitForSeconds(spawnDelay);
        appleSlider.value = maxSliderValue;

        GameObject respawnedPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        PlayerMovementController respawnedMC = respawnedPlayer.GetComponent<PlayerMovementController>();
        respawnedMC.isDoubleJumpEnabled = _playerMC.isDoubleJumpEnabled;
        respawnedMC.isWallJumpEnabled = _playerMC.isWallJumpEnabled;

        if (_chaser != null)
        {
            isPlayerDead = false;
            _chaser.GetComponent<DamagingObject>().ResetPosition();
        }

        GameObject clone = Instantiate(spawnParticlePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Destroy(clone, 3.0f);

        if (AppleBarSlider.instance != null)
        {
            yield return new WaitForSeconds(5f);
            AppleBarSlider.instance.DecreaseAppleSlider();
        }

    }

    public IEnumerator EndLevel()
    {
        AppleBarSlider.instance.StopAllCoroutines();

        //audio
        _friendAnimator.SetBool("isFree", true);

        yield return new WaitForSeconds(endDelay);

        GameObject friendParticles = Instantiate(endParticlePrefab, friendToSave.transform.position, Quaternion.identity);
        Destroy(friendToSave, 1.0f);
        Destroy(friendParticles, 3.0f);

        yield return new WaitForSeconds(3f);

        endCanvas.SetActive(true);
        Time.timeScale = 0f;

    }

    public IEnumerator SpawnPlatform(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(waitBeforeSpawning);

        Instantiate(fallingPlatform, spawnPosition, fallingPlatform.transform.rotation);

    }

    public IEnumerator SpawnApple(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(waitBeforeSpawningApple);

        GameObject newApple = Instantiate(applePrefab, spawnPosition, applePrefab.transform.rotation);
        newApple.tag = "BossApple";
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("LoadSaved", 1);
        PlayerPrefs.SetInt("SavedGame", SceneManager.GetActiveScene().buildIndex + 1);

        if(floatingTextPrefab)
            ShowFloatingText();
    }

    void ShowFloatingText()
    {
        Instantiate(floatingTextPrefab, saveButton.transform.position, Quaternion.identity, endCanvas.transform);
    }

}

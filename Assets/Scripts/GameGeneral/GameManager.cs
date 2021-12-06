using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CinemachineVirtualCamera virtualCamera;

    [Header("Start Level")]
    [HideInInspector] public bool bossLevel;
    private PlayerMovementController _playerMC;
    [HideInInspector] public bool gunAvailable;

    [Space]
    [Header("Spawning")]
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public float spawnDelay = 1.0f;
    public GameObject spawnParticlePrefab;
    [HideInInspector] public bool isChaserActive;
    [HideInInspector] public bool isPlayerDead;
    private GameObject _chaser;

    [Space]
    [Header("Slider")]
    public Slider appleSlider;
    public int maxSliderValue = 20;
    public GameObject laserCanvas;
    public Slider laserSlider;
    private GameObject _player;

    [Space]
    [Header("PlatformManager")]
    public GameObject fallingPlatform;
    public float waitBeforeSpawning = 5.0f;

    [Space]
    [Header("AppleSpawner")]
    public GameObject applePrefab;
    public float waitBeforeSpawningApple = 7.0f;

    [Space]
    [Header("End Level")]
    public float endDelay = 2.0f;
    private int _sceneIndex;
    public GameObject friendToSave;
    public GameObject endParticlePrefab;
    public GameObject endCanvas;
    public Text floatingTextPrefab;
    public Button saveButton;
    private Animator _friendAnimator;
    [SerializeField] private float _waitBeforeEndCanvas = 3.0f;

    [Space]
    [Header("Boss Level")]
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _transformedBoss;

    private void Awake()
    {
        instance = this;
        _playerMC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_sceneIndex != 4)
        {
            _player.GetComponent<DestroyOffScreen>().enabled = false;

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

        if (laserCanvas != null)
            laserCanvas.SetActive(false);

        if (friendToSave != null)
            _friendAnimator = friendToSave.GetComponent<Animator>();

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
        laserCanvas.SetActive(true);
        laserSlider.value = LaserBarSlider.instance.currentSliderValue;
        LaserBarSlider.instance.DecreaseLaserSlider();
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        appleSlider.value = maxSliderValue;

        GameObject respawnedPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        _player = respawnedPlayer;

        PlayerMovementController respawnedMC = respawnedPlayer.GetComponent<PlayerMovementController>();
        respawnedMC.isDoubleJumpEnabled = _playerMC.isDoubleJumpEnabled;
        respawnedMC.isWallJumpEnabled = _playerMC.isWallJumpEnabled;
        _playerMC = respawnedMC;

        if (spawnPoint.GetComponent<Checkpoint>() != null && spawnPoint.GetComponent<Checkpoint>().cam != virtualCamera)
        {
            CinemachineVirtualCamera prevCam = virtualCamera;

            virtualCamera = spawnPoint.GetComponent<Checkpoint>().cam;
            prevCam.Priority = 1;
            virtualCamera.Priority = 2;
        }

        if (!isChaserActive)
            virtualCamera.Follow = respawnedPlayer.transform;

        if (_chaser != null)
        {
            isPlayerDead = false;
            _chaser.GetComponent<DamagingObject>().ResetPosition();
        }

        AudioManager.instance.Play("PlayerRespawn");
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

        if (laserCanvas != null && laserCanvas.activeSelf == true)
        {
            LaserBarSlider.instance.StopAllCoroutines();
            LaserBarSlider.instance.DestroyGun();
            laserCanvas.SetActive(false);
        }

        if (friendToSave != null)
        {
            Instantiate(endParticlePrefab, friendToSave.transform.position, Quaternion.identity);
            _friendAnimator.SetBool("isFree", true);
        }

        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _player.GetComponent<Animator>().SetBool("HasWon", true);
        _playerMC.enabled = false;

        AudioManager.instance.FadeOut("Theme");
        yield return new WaitForSeconds(endDelay);
        AudioManager.instance.Play("LevelEnd");

        if (_sceneIndex != 4)
            Destroy(friendToSave, _waitBeforeEndCanvas - 1f);
        else
        {
            Vector3 bossPos = _boss.transform.position;
            bossPos.y = _boss.transform.position.y + 0.5f;

            GameObject bossParticles = Instantiate(endParticlePrefab, bossPos, Quaternion.identity);
            Instantiate(_transformedBoss, bossPos, Quaternion.identity);
            Destroy(_boss);
            Destroy(bossParticles, 3.0f);
        }

        yield return new WaitForSeconds(_waitBeforeEndCanvas);

        if (_sceneIndex == 4)
            AudioManager.instance.Play("Victory");
        else
            AudioManager.instance.Play("EndMenu");

        endCanvas.SetActive(true);
    }

    //FEEDBACK: game manager ne bi trebao bit zaduzen za ovakve stvari
    // to je ili platform spawner ili neki level manager
    public IEnumerator SpawnPlatform(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(waitBeforeSpawning);

        Instantiate(fallingPlatform, spawnPosition, fallingPlatform.transform.rotation);

    }

    //FEEDBACK: isto kao i gore
    public IEnumerator SpawnApple(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(waitBeforeSpawningApple);

        GameObject newApple = Instantiate(applePrefab, spawnPosition, applePrefab.transform.rotation);
        newApple.tag = "BossApple";
        newApple.transform.parent = GameObject.FindGameObjectWithTag("ApplesParentObject").transform;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("LoadSaved", 1);
        PlayerPrefs.SetInt("SavedGame", SceneManager.GetActiveScene().buildIndex + 1);

        if (floatingTextPrefab)
        {
            AudioManager.instance.Play("Apple");
            ShowFloatingText();
        }
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("Apple");
        PlayerPrefs.SetInt("LoadSaved", 1);
        PlayerPrefs.SetInt("SavedGame", SceneManager.GetActiveScene().buildIndex);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

    }

    void ShowFloatingText()
    {
        Instantiate(floatingTextPrefab, saveButton.transform.position, Quaternion.identity, endCanvas.transform);
    }

}

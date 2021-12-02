using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject levelLoader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioManager.instance.Play("Apple");
        AudioManager.instance.TurnUp("Theme");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    void Pause()
    {
        AudioManager.instance.TurnDown("Theme");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    //public void LoadMenu()
    //{
    //    _am.Play("Apple");
    //    Time.timeScale = 1.0f;
    //    levelLoader.GetComponent<LevelLoader>().LoadLevel(0);
    //}

    //public void QuitGame()
    //{
    //    _am.Play("Apple");
    //    PlayerPrefs.SetInt("LoadSaved", 1);
    //    PlayerPrefs.SetInt("SavedGame", SceneManager.GetActiveScene().buildIndex);

    // #if UNITY_EDITOR
    //    UnityEditor.EditorApplication.isPlaying = false;
    // #else
    //     Application.Quit();
    // #endif

    //}

    //public void RestartLevel()
    //{
    //    _am.Play("Apple");
    //    Time.timeScale = 1.0f;
    //    levelLoader.GetComponent<LevelLoader>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
    //}
}

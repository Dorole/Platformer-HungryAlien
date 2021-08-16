using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text floatingLoadText;
    public Button loadButton;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("LoadSaved") == 1)
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedGame"));
        else
        {
            Instantiate(floatingLoadText, loadButton.transform.position, Quaternion.identity, transform);
        }

    }
}

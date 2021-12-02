using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text floatingLoadText;
    public Button loadButton;
    public GameObject levelLoader;

    //public void PlayGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

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
        {
            AudioManager.instance.Play("Apple");
            levelLoader.GetComponent<LevelLoader>().LoadLevel(PlayerPrefs.GetInt("SavedGame"));
            //SceneManager.LoadScene(PlayerPrefs.GetInt("SavedGame"));
        }
        else
        {
            Vector3 buttonPos = loadButton.GetComponent<RectTransform>().anchoredPosition;

            Instantiate(floatingLoadText, buttonPos, Quaternion.identity, transform);
            AudioManager.instance.Play("LoadFailed");
        }

    }
}

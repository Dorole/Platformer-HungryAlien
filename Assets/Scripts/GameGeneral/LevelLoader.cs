using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _progressText;

    public void LoadLevel(int sceneIndex)
    {
        AudioManager.instance.Play("Apple");
        Time.timeScale = 1f;

        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        _loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            _slider.value = progress;
            _progressText.text = progress * 100f + "%";

            yield return null;
        }

    }
}

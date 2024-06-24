using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoadScreenController : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1f;

    private VisualElement rootVE;
    private ProgressBar progressBar;

    void Start()
    {
        rootVE = GetComponent<UIDocument>().rootVisualElement;
        progressBar = rootVE.Q<ProgressBar>("progress");

        string lastPlayedLevelName =
            LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName;
        StartLoad(lastPlayedLevelName);
    }

    public void StartLoad(string levelName)
    {
        StartCoroutine(LoadLevelAsync(levelName));
    }

    private IEnumerator LoadLevelAsync(string levelName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            progressBar.value = progressValue * 100;

            if (loadOperation.progress >= 0.9f)
            {
                yield return StartCoroutine(FadeIn());
                loadOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        Time.timeScale = 1;

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            rootVE.style.opacity = alpha;
            yield return null;
        }
    }

}

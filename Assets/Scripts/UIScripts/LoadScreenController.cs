using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoadScreenController : MonoBehaviour, AdsEventCallback
{
    [SerializeField] private float fadeSpeed = 1f;

    [Header("AdMob params")]
    [SerializeField] private int chanceToShowAd;

    private VisualElement rootVE;
    private ProgressBar progressBar;

    // admob fields
    private AdmobInterstitialAd interstitialAd;
    private string navigateLevelAfterAds;

    void Start()
    {
        interstitialAd = new AdmobInterstitialAd();
        interstitialAd.SetAdsCallback(this);

        rootVE = GetComponent<UIDocument>().rootVisualElement;
        progressBar = rootVE.Q<ProgressBar>("progress");

        string lastPlayedLevelName =
            LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName;
        StartLoad(lastPlayedLevelName);
    }

    public void StartLoad(string levelName)
    {
        navigateLevelAfterAds = levelName;

        bool shouldShowAd = Random.Range(1, chanceToShowAd) == 1;
        if (!shouldShowAd || !interstitialAd.ShowInterstitialAd())
        {
            StartCoroutine(LoadLevelAsync(levelName));
        }
    }

    private IEnumerator LoadLevelAsync(string levelName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            progressBar.value = progressValue * 100;
            progressBar.title = (progressValue * 100).ToString() + '%';

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

    public void OnStandartAdsClose()
    {
        StartCoroutine(LoadLevelAsync(navigateLevelAfterAds));
    }

    public void OnRewardedAdsClose()
    {
       
    }

}

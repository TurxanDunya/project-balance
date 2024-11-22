using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SurpriseStar : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private int timeToShowSurpise;
    [SerializeField] private int surpriseCount = 1000;

    private GameCore gameCore;
    private HomeScreenController homeScreenController;
    private SurpriseSaveSystem surpriseSaveSystem;
    private CoinManager coinManager;

    // UI
    private VisualElement rootSurpriseSystemVE;
    private Button continueBtn;

    private void Start()
    {
        gameCore = FindAnyObjectByType<GameCore>();
        homeScreenController = FindAnyObjectByType<HomeScreenController>();
        surpriseSaveSystem = GetComponent<SurpriseSaveSystem>();
        coinManager = FindAnyObjectByType<CoinManager>();

        rootSurpriseSystemVE = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("RootVE");
        continueBtn = rootSurpriseSystemVE.Q<Button>("continue_btn");
        continueBtn.clicked += () => HideStarSurprisePopUp();

        CheckAndStartCoroutine();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void CheckAndStartCoroutine()
    {
        SurpriseData surpriseData = surpriseSaveSystem.GetSurpriseData();
        if (!surpriseData.isSurpriseStarWatched)
        {
            StartCoroutine(UpdateSecond());
        }
    }

    private IEnumerator UpdateSecond()
    {
        while (timeToShowSurpise >= 0)
        {
            if (homeScreenController && homeScreenController.IsDisplayFlex())
            {
                yield return new WaitForSeconds(1);
            }
            else if (gameCore.IsAboutToWin || gameCore.IsWin)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                timeToShowSurpise--;
                yield return new WaitForSeconds(1);
            }
        }

        rootSurpriseSystemVE.style.display = DisplayStyle.Flex;

        SurpriseData surpriseData = surpriseSaveSystem.GetSurpriseData();
        surpriseData.isSurpriseStarWatched = true;
        surpriseSaveSystem.SetSurpriseData(surpriseData);
        surpriseSaveSystem.SaveSurpriseData();

        coinManager.AddCoin(surpriseCount);

        gameCore.ProcessWinEvent();
    }

    private void HideStarSurprisePopUp()
    {
        rootSurpriseSystemVE.style.display = DisplayStyle.None;
    }

}
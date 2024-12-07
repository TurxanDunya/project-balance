using UnityEngine;
using UnityEngine.UIElements;

public class HomeScreenController : MonoBehaviour, AdsEventCallback, IControllerTemplate
{
    [SerializeField] private StateChanger stateChanger;

    [SerializeField] private float buttonAnimationsMoveSpeed = 0.5f;

    private ButtonShaker buttonShaker;

    // button animation positions
    private float aboutUsButtonPos = 200f;
    private bool isForwardDirection = true;

    private VisualElement rootElement;
    private VisualElement topVE;
    private VisualElement leftSideVE;
    private VisualElement rightSideVE;
    private Button settingsButton;
    private Button tapToPlayButton;
    private Button aboutUsButton;
    private Button addStarAdsButton;

    private AdmobRewardedAd rewardedAd;
    private CoinManager coinManager;

    private bool isHomePageEnabled = true;

    private void Start()
    {
        buttonShaker = GetComponent<ButtonShaker>();
        coinManager = FindAnyObjectByType<CoinManager>();
        MakeBindings();

        ShakeAdsButton(); // Should be at the end because of coroutine

        SafeArea.ApplySafeArea(topVE);

        if (!rewardedAd.CanShowRewardedAds())
        {
            rewardedAd.LoadAd();
        }
    }

    private void OnDisable()
    {
        addStarAdsButton.clicked -= ShowAdsAndAddCoin;

        settingsButton.clicked -= ShowSettingsUI;
        tapToPlayButton.clicked -= ChangeStateForInGameUI;
        aboutUsButton.clicked -= ShowAboutUsUI;

        rootElement = null;
        topVE = null;
        leftSideVE = null;
        rightSideVE = null;
        settingsButton = null;
        tapToPlayButton = null;
        aboutUsButton = null;
        addStarAdsButton = null;
    }

    public void MakeBindings()
    {
        rewardedAd = new AdmobRewardedAd();
        rewardedAd.SetAdsCallback(this);
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        topVE = rootElement.Q<VisualElement>("topVE");
        leftSideVE = topVE.Q<VisualElement>("left_side_ve");
        settingsButton = leftSideVE.Q<Button>("settings_btn");
        aboutUsButton = leftSideVE.Q<Button>("about_us_btn");

        rightSideVE = topVE.Q<VisualElement>("right_side_ve");
        addStarAdsButton = rightSideVE.Q<Button>("add_star_ads_btn");
        addStarAdsButton.clicked += ShowAdsAndAddCoin;

        tapToPlayButton = rootElement.Q<Button>("tapToPlayButton");

        settingsButton.clicked += ShowSettingsUI;
        tapToPlayButton.clicked += ChangeStateForInGameUI;
        aboutUsButton.clicked += ShowAboutUsUI;
    }

    private void FixedUpdate()
    {
        DefineAboutUsButtonPosition();
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        return isHomePageEnabled;
    }

    private void ShakeAdsButton()
    {
        buttonShaker.ShakeButton(addStarAdsButton);
    }

    private void ChangeStateForInGameUI()
    {
        stateChanger.ChangeStateToInGameUI();
        isHomePageEnabled = false;
    }

    private void ShowSettingsUI()
    {
        stateChanger.ChangeStateFromMainUIToSettingsUI();
    }

    private void ShowAboutUsUI()
    {
        stateChanger.ShowAboutUsUI();
    }

    private void ShowAdsAndAddCoin()
    {
        if (rewardedAd.CanShowRewardedAds())
        {
            rewardedAd.ShowAd();
        }
        else {
            rewardedAd.LoadAd();
        }
    }

    private void DefineAboutUsButtonPosition()
    {
        if (isForwardDirection)
        {
            aboutUsButtonPos++;
            aboutUsButton.style.marginLeft = aboutUsButtonPos * buttonAnimationsMoveSpeed;
            if (aboutUsButton.style.marginLeft.value.value >= 900)
            {
                isForwardDirection = false;
            }
        }
        else if (!isForwardDirection)
        {
            aboutUsButtonPos--;
            aboutUsButton.style.marginLeft = aboutUsButtonPos * buttonAnimationsMoveSpeed;
            if (aboutUsButton.style.marginLeft.value.value <= 100)
            {
                isForwardDirection = true;
            }
        }
    }

    public void OnStandartAdsClose()
    {
       
    }

    public void OnRewardedAdsClose(double reward)
    {
        long stars = (long) reward;
        coinManager.AddCoin(stars);
    }

    public bool IsDisplayFlex()
    {
        if (rootElement == null)
        {
            return true;
        }

        return rootElement.style.display == DisplayStyle.Flex;
    }

    public void SetDisplayFlex()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootElement.style.display = DisplayStyle.None;
    }

    public bool IsOverUI()
    {
        return rootElement.style.display == DisplayStyle.Flex;
    }

    private void OnDestroy()
    {
        rewardedAd.Destroy();
    }

}

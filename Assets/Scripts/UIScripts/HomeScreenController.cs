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

        addStarAdsButton.RegisterCallback<PointerDownEvent>(ShowAdsAndAddCoinPressed, TrickleDown.TrickleDown);
        addStarAdsButton.RegisterCallback<PointerUpEvent>(ShowAdsAndAddCoinReleased);

        tapToPlayButton = rootElement.Q<Button>("tapToPlayButton");

        settingsButton.RegisterCallback<PointerDownEvent>(ShowSettingsUIPressed, TrickleDown.TrickleDown);
        settingsButton.RegisterCallback<PointerUpEvent>(ShowSettingsUIReleased);

        tapToPlayButton.RegisterCallback<PointerDownEvent>(ChangeStateForInGameUIPressed, TrickleDown.TrickleDown);
        tapToPlayButton.RegisterCallback<PointerUpEvent>(ChangeStateForInGameUIReleased);

        aboutUsButton.RegisterCallback<PointerDownEvent>(ShowAboutUsUIPressed, TrickleDown.TrickleDown);
        aboutUsButton.RegisterCallback<PointerUpEvent>(ShowAboutUsUIReleased);
    }

    private void OnDisable()
    {
        addStarAdsButton.UnregisterCallback<PointerDownEvent>(ShowAdsAndAddCoinPressed);
        addStarAdsButton.UnregisterCallback<PointerUpEvent>(ShowAdsAndAddCoinReleased);

        settingsButton.UnregisterCallback<PointerDownEvent>(ShowSettingsUIPressed);
        settingsButton.UnregisterCallback<PointerUpEvent>(ShowSettingsUIReleased);

        tapToPlayButton.UnregisterCallback<PointerDownEvent>(ChangeStateForInGameUIPressed);
        tapToPlayButton.UnregisterCallback<PointerUpEvent>(ChangeStateForInGameUIReleased);

        aboutUsButton.UnregisterCallback<PointerDownEvent>(ShowAboutUsUIPressed);
        aboutUsButton.UnregisterCallback<PointerUpEvent>(ShowAboutUsUIReleased);
    }

    private void FixedUpdate()
    {
        DefineAboutUsButtonPosition();
    }

    private void ShakeAdsButton()
    {
        buttonShaker.ShakeButton(addStarAdsButton);
    }

    private void ChangeStateForInGameUIPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ChangeStateForInGameUIReleased(PointerUpEvent ev)
    {
        stateChanger.ChangeStateToInGameUI();
        InputManager.isOverUI = false;
    }

    private void ShowSettingsUIPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ShowSettingsUIReleased(PointerUpEvent ev)
    {
        stateChanger.ChangeStateFromMainUIToSettingsUI();
        InputManager.isOverUI = false;
    }

    private void ShowAboutUsUIPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ShowAboutUsUIReleased(PointerUpEvent ev)
    {
        stateChanger.ShowAboutUsUI();
        InputManager.isOverUI = false;
    }

    private void ShowAdsAndAddCoinPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ShowAdsAndAddCoinReleased(PointerUpEvent ev)
    {
        if (rewardedAd.CanShowRewardedAds())
        {
            rewardedAd.ShowAd();
        }
        else {
            rewardedAd.LoadAd();
        }

        InputManager.isOverUI = false;
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

    public void OnStandartAdsClose() {}

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

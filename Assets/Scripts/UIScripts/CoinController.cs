using UnityEngine;
using UnityEngine.UIElements;

public class CoinController : MonoBehaviour, IControllerTemplate
{
    private CoinManager coinManager;

    private VisualElement rootElement;
    private VisualElement collectedCoinsVE;
    private Label coinCountLabel;

    private void Awake()
    {
        coinManager = FindAnyObjectByType<CoinManager>();
    }

    private void OnEnable()
    {
        coinManager.OnCoinCountChangeEvent += UpdateInGameCoinValue;
    }

    private void OnDisable()
    {
        coinManager.OnCoinCountChangeEvent -= UpdateInGameCoinValue;

        rootElement = null;
        collectedCoinsVE = null;
        coinCountLabel = null;
    }

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        collectedCoinsVE = rootElement.Q<VisualElement>("CollectedCoins");
        coinCountLabel = collectedCoinsVE.Q<Label>("CoinCount");

        UpdateInGameCoinValue(coinManager.CoinCount);
        SafeArea.ApplySafeArea(rootElement);
    }

    private void UpdateInGameCoinValue(long cointCount)
    {
        coinCountLabel.text = cointCount.ToString();
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

}

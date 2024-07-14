using UnityEngine;
using UnityEngine.UIElements;

public class CoinController : MonoBehaviour
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
    }

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        collectedCoinsVE = rootElement.Q<VisualElement>("CollectedCoins");
        coinCountLabel = collectedCoinsVE.Q<Label>("CoinCount");

        UpdateInGameCoinValue(coinManager.CoinCount);
    }

    private void UpdateInGameCoinValue(long cointCount)
    {
        coinCountLabel.text = cointCount.ToString();
    }

}

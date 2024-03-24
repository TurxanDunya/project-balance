using UnityEngine;
using UnityEngine.UIElements;

public class CoinController : MonoBehaviour
{
    private Coin coin;
    private CoinManager coinManager;

    private VisualElement rootElement;
    private VisualElement collectedCoinsVE;
    private Label label;

    private void Awake()
    {
        coin = FindAnyObjectByType<Coin>();
        coinManager = FindAnyObjectByType<CoinManager>();
    }

    private void OnEnable()
    {
        coin.OnCoinAddEvent += UpdateInGameCoinValue;
    }

    private void OnDisable()
    {
        coin.OnCoinAddEvent -= UpdateInGameCoinValue;
    }

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        collectedCoinsVE = rootElement.Q<VisualElement>("CollectedCoins");
        label = collectedCoinsVE.Q<Label>("CoinCount");

        label.text = coinManager.CoinCount.ToString();
    }

    private void UpdateInGameCoinValue()
    {
        label.text = coinManager.CoinCount.ToString();
    }
    
}

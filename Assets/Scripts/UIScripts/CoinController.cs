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
        coinManager.OnCoinAddEvent += UpdateInGameCoinValue;
    }

    private void OnDisable()
    {
        coinManager.OnCoinAddEvent -= UpdateInGameCoinValue;
    }

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        collectedCoinsVE = rootElement.Q<VisualElement>("CollectedCoins");
        coinCountLabel = collectedCoinsVE.Q<Label>("CoinCount");

        coinCountLabel.text = coinManager.CoinCount.ToString();
    }

    private void UpdateInGameCoinValue()
    {
        coinCountLabel.text = coinManager.CoinCount.ToString();
    }

}

using UnityEngine;
using UnityEngine.UIElements;

public class WelcomeTutorialController : MonoBehaviour
{
    [SerializeField] private GameObject self;

    private VisualElement rootElement;
    private Button gotItButton;
    private VisualElement moveFingerImageVE;

    private bool isMoveFingerImageVEEnabled = true;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        moveFingerImageVE = rootElement.Q<VisualElement>("MoveFingerImageVE");
        gotItButton = moveFingerImageVE.Q<Button>("GotItButton");
        

        gotItButton.clicked += DisableHowToMoveCubeVE;
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        return isMoveFingerImageVEEnabled;
    }

    private void DisableHowToMoveCubeVE()
    {
        if (isMoveFingerImageVEEnabled)
        {
            rootElement.Remove(moveFingerImageVE);
            isMoveFingerImageVEEnabled = false;
            Destroy(self);
        }
    }

}

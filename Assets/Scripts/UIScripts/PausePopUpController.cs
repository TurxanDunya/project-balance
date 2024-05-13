using UnityEngine;
using UnityEngine.UIElements;

public class PausePopUpController : MonoBehaviour
{
    private VisualElement rootElement;
    private VisualElement pausePopUpVE;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        pausePopUpVE = rootElement.Q<VisualElement>("PausePopUpVE");
    }

    public void Show()
    {
        pausePopUpVE.style.display = DisplayStyle.Flex;
    }

}

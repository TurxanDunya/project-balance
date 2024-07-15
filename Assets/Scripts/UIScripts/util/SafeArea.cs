using UnityEngine;
using UnityEngine.UIElements;

public class SafeArea
{
    public static void ApplySafeArea(VisualElement rootElement)
    {
        Rect safeArea = Screen.safeArea;

        float safeAreaXMin = safeArea.xMin / Screen.width;
        float safeAreaXMax = safeArea.xMax / Screen.width;
        float safeAreaYMin = safeArea.yMin / Screen.height;
        float safeAreaYMax = safeArea.yMax / Screen.height;

        rootElement.style.marginLeft = new Length(safeAreaXMin * 100, LengthUnit.Percent);
        rootElement.style.marginRight = new Length((1 - safeAreaXMax) * 100, LengthUnit.Percent);
        rootElement.style.marginTop = new Length(safeAreaYMin * 100, LengthUnit.Percent);
        rootElement.style.marginBottom = new Length((1 - safeAreaYMax) * 100, LengthUnit.Percent);
    }
}

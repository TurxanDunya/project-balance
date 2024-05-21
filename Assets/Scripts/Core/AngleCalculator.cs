using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float currentX = 0;
    private float currentZ = 0;

    private void Start()
    {
        currentX = transform.rotation.eulerAngles.x;
        currentZ = transform.rotation.eulerAngles.z;
    }

    public int GetPlatformAngle()
    {
        var eulerAnglesX = target.rotation.eulerAngles.x;
        var eulerAnglesZ = target.rotation.eulerAngles.z;

        var angleX = (int)Mathf.Abs(Mathf.DeltaAngle(currentX, eulerAnglesX));
        var angleZ = (int)Mathf.Abs(Mathf.DeltaAngle(currentZ, eulerAnglesZ));
        return Mathf.Max(angleX, angleZ);
    }

}

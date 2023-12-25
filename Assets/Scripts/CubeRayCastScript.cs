using UnityEngine;

public class CubeRayCastScript : MonoBehaviour
{
    public float raycastDistance = 500f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, raycastDistance))
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position - transform.up * raycastDistance);
            }
        }
    }

    public bool IsHittingPlatform()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
           return false;
        }

        if (!hit.collider.CompareTag("MainPlatform"))
        {
            return false;
        }

        return true;
    }

}

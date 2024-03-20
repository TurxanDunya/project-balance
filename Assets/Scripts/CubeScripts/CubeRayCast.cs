using UnityEngine;

public class CubeRayCastScript : MonoBehaviour
{
    public float raycastDistance = 50f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public bool IsHittingPlatform()
    { 
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
            return false;
        }

        if (!hit.collider.CompareTag(Constants.MAIN_PLATFORM) &&
                !hit.collider.CompareTag(Constants.PLAYABLE_CUBE))
        {
            return false;
        }

        return true;
    }

    public void UpdateLineRendererStatus()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }
        }
    }

}

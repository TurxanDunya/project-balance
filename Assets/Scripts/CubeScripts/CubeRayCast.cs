using UnityEngine;

public class CubeRayCast : MonoBehaviour
{
    public float raycastDistance = 5f;

    private RaycastHit hit;
    private LineRenderer lineRenderer;
    private readonly float minDistance = 0.05f;

    private void OnEnable()
    {
        UpdateRaycastHitPosition();
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public Vector3 GetLineRendererHitPosition()
    {
        return hit.point;
    }

    public string GetLineRendererHitObjectTag()
    {
        return hit.collider.tag;
    }

    public void UpdateRaycastHitPosition()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            if (hit.distance <= minDistance)
            {
                return;
            }

            this.hit = hit;
        }
    }

    public bool IsHittingPlayables()
    {
        UpdateRaycastHitPosition();
        if (!hit.collider.CompareTag(TagConstants.MAIN_PLATFORM) &&
                !hit.collider.CompareTag(TagConstants.MAIN_PLATFORM_COLLIDER) &&
                !hit.collider.CompareTag(TagConstants.PLAYABLE_CUBE) &&
                !hit.collider.CompareTag(TagConstants.DROPPED_CUBE) &&
                !hit.collider.CompareTag(TagConstants.STAR) &&
                !hit.collider.CompareTag(TagConstants.MAGNET))
        {
            return false;
        }

        return true;
    }

    public void UpdateLineRendererPosition()
    {
        if (lineRenderer == null)
        {
            return;
        }

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            if (hit.distance <= minDistance)
            {
                return;
            }

            lineRenderer.enabled = true;
            this.hit = hit;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public Quaternion GetLineRendererHitRotation()
    {
        return hit.transform.rotation;
    }

}

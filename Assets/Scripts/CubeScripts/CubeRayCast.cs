using UnityEngine;

public class CubeRayCast : MonoBehaviour
{
    public float raycastDistance = 50f;

    private LineRenderer lineRenderer;
    private Vector3 hitPosition;
    private float minDistance = 0.05f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public Vector3 GetLineRendererHitPosition()
    {
        return hitPosition;
    }

    public void UpdateRaycastHitPosition()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            if (hit.distance <= minDistance)
            {
                return;
            }

            hitPosition = hit.point;
        }
    }

    public bool IsHittingPlayables()
    {
        if (!Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            return false;
        }

        if (!hit.collider.CompareTag(TagConstants.MAIN_PLATFORM) &&
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
            hitPosition = hit.point;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hitPosition);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public Quaternion GetLineRendererHitRotation()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            return hit.transform.rotation;
        }

        return Quaternion.identity;
    }

}

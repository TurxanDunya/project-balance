using UnityEngine;

public class CubeRayCast : MonoBehaviour
{
    public float raycastDistance = 50f;

    private LineRenderer lineRenderer;
    private LineRendererAnimator lineRendererAnimator;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRendererAnimator = GetComponent<LineRendererAnimator>();
    }

    public bool IsHittingPlatform()
    {
        if (!Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            return false;
        }

        if (!hit.collider.CompareTag(TagConstants.MAIN_PLATFORM) &&
                !hit.collider.CompareTag(TagConstants.PLAYABLE_CUBE))
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
            lineRenderer.enabled = true;

            if (!lineRendererAnimator.isAnimateToDownCoroutineFinished)
            {
                StartCoroutine(lineRendererAnimator.AnimateToDown(transform.position, hit.point));
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public Vector3 GetLineRendererHitPosition()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            return hit.point;
        }

        return new Vector3(0, 0, 0);
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

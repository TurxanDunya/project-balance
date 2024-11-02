using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class CameraPosition : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float normalizedDegreeToMoveOppsiteSide = 0.2f;
    [SerializeField] private PathType pathType;

    private GameObject platformObj;
    private Vector3[] positions;
    private int currentTargetIndex = 1;

    private void Start()
    {
        platformObj = GameObject.FindGameObjectWithTag(TagConstants.MAIN_PLATFORM);
        GameObject predefinedPositions = GameObject.FindGameObjectWithTag(TagConstants.PREDEFINED_POSITIONS);
        positions = predefinedPositions.GetComponentsInChildren<Transform>()
            .Select(position => position.position)
            .ToArray();

        StartCoroutine(ChangePositionBasedOnPlatform());
    }

    private IEnumerator ChangePositionBasedOnPlatform()
    {
        while (true)
        {
            Quaternion platformRotation = platformObj.transform.rotation;
            int targetIndex = GetPositionIndexFromRotation(platformRotation);

            if (targetIndex != currentTargetIndex)
            {
                currentTargetIndex = targetIndex;
                Vector3 targetPosition = positions[targetIndex];

                Tweener moveTween = transform.DOPath(new Vector3[] { targetPosition }, 3f, pathType);

                while (moveTween.IsActive() && !moveTween.IsComplete())
                {
                    transform.LookAt(platformObj.transform.position);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private int GetPositionIndexFromRotation(Quaternion rotation)
    {
        float xRot = rotation.x;
        float zRot = rotation.z;

        if (xRot > normalizedDegreeToMoveOppsiteSide) return 3;
        if (zRot > normalizedDegreeToMoveOppsiteSide) return 2;
        if (xRot < -normalizedDegreeToMoveOppsiteSide) return 1;
        if (zRot < -normalizedDegreeToMoveOppsiteSide) return 4;
        return 1;
    }

}

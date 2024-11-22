using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class CameraPosition : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float normalizedDegreeToMoveOppsiteSide = 0.2f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private PathType pathType;

    private GameObject platformObj;
    private Vector3[] positions;
    private int currentTargetIndex = 1;

    private AudioSource cameraChangePositionSFX;

    private void Start()
    {
        cameraChangePositionSFX = GetComponent<AudioSource>();

        platformObj = GameObject.FindGameObjectWithTag(TagConstants.MAIN_PLATFORM);
        GameObject predefinedPositions =
            GameObject.FindGameObjectWithTag(TagConstants.PREDEFINED_POSITIONS);
        positions = predefinedPositions.GetComponentsInChildren<Transform>()
            .Select(position => position.position)
            .ToArray();

        StartCoroutine(ChangePositionBasedOnPlatform());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ChangePositionBasedOnPlatform()
    {
        while (true)
        {
            Quaternion platformRotation = platformObj.transform.rotation;
            int targetIndex = GetPositionIndexFromRotation(platformRotation);

            if (targetIndex != currentTargetIndex && targetIndex != 0)
            {
                currentTargetIndex = targetIndex;
                Vector3 targetPosition = positions[targetIndex];

                Tweener moveTween = transform.DOPath(new Vector3[] { targetPosition }, speed, pathType);
                cameraChangePositionSFX.Play();

                while (moveTween.IsActive() && !moveTween.IsComplete())
                {
                    transform.LookAt(platformObj.transform.position);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(1f);
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
        return 0; // 0 means position should not be changed
    }

}

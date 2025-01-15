using System.Collections;

using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

public class CameraPosition : MonoBehaviour
{
    [Header("Listening on Channels")]
    [SerializeField] private CameraMovementEventChannelSO cameraMovementEventChannel;

    [Header("Params")]
    [SerializeField] private float normalizedDegreeToMoveOppsiteSide = 0.2f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private PathType pathType;

    private GameObject platformObj;
    private Transform[] cameraTransforms;
    private int currentTargetIndex = 1;

    private AudioSource cameraChangePositionSFX;

    private void OnEnable()
    {
        cameraMovementEventChannel.CameraMoveEvent += StartChangePositionCoroutine;
    }

    private void OnDisable()
    {
        cameraMovementEventChannel.CameraMoveEvent -= StartChangePositionCoroutine;
    }

    private void Start()
    {
        cameraChangePositionSFX = GetComponent<AudioSource>();

        platformObj = GameObject.FindGameObjectWithTag(TagConstants.MAIN_PLATFORM);
        GameObject predefinedPositions =
            GameObject.FindGameObjectWithTag(TagConstants.PREDEFINED_POSITIONS);
        cameraTransforms = predefinedPositions.GetComponentsInChildren<Transform>();
    }

    private void StartChangePositionCoroutine(Direction direction)
    {
        StartCoroutine(ChangePosition(direction));
    }

    private IEnumerator ChangePosition(Direction direction)
    {
        currentTargetIndex = DefineNextCameraPositionIndex(direction);
        Transform targetTransform = cameraTransforms[currentTargetIndex];

        Tweener moveTween = transform.DOPath(new Vector3[] { targetTransform.position }, speed, pathType);
        cameraChangePositionSFX.Play();

        while (moveTween.IsActive() && !moveTween.IsComplete())
        {
            transform.LookAt(platformObj.transform.position);
            yield return null;
        }

        yield return null;
    }

    private int DefineNextCameraPositionIndex(Direction direction)
    {
        if (direction == Direction.ACROSS && currentTargetIndex <= 2)
        {
            return currentTargetIndex += 2;
        }
        else if (direction == Direction.ACROSS && currentTargetIndex <= 4)
        {
            return currentTargetIndex -= 2;
        }

        if (direction == Direction.LEFT && currentTargetIndex < 4)
        {
            return currentTargetIndex += 1;
        }
        else if (direction == Direction.LEFT && currentTargetIndex == 4)
        {
            return 1;
        }

        if (direction == Direction.RIGHT && currentTargetIndex > 1)
        {
            return currentTargetIndex -= 1;
        }
        else if (direction == Direction.RIGHT && currentTargetIndex == 1)
        {
            return 4;
        }

        return currentTargetIndex;
    }

    public enum Direction { 
        ACROSS, 
        LEFT, 
        RIGHT 
    }

}

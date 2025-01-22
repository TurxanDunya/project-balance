using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CameraMovementEventChannel", menuName = "EventChannels/CameraMovement")]
public class CameraMovementEventChannelSO : ScriptableObject
{
    public UnityAction<CameraPosition.Direction> CameraMoveEvent;

    public void RaiseEvent(CameraPosition.Direction direction)
    {
        if (CameraMoveEvent != null)
        {
            CameraMoveEvent.Invoke(direction);
        }
    }

}

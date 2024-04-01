using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static event Action CubeLanded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYABLE_CUBE))
        {
            CubeLanded?.Invoke();
        }
    }
}

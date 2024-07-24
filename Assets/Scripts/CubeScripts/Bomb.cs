using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] private float affectDistance;

    private void OnEnable()
    {
        Platform.BombLanded += Blast;
    }

    private void OnDisable()
    {
        Platform.BombLanded -= Blast;
    }

    public void Blast()
    {
        GameObject[] affectedObjects = GameObject.FindGameObjectsWithTag(TagConstants.DROPPED_CUBE);

        foreach (GameObject affectedObject in affectedObjects)
        {
            Vector3 bombPosition = transform.position;
            Vector3 otherObjectPosition = affectedObject.transform.position;
            float distanceBetween = Vector3.Distance(bombPosition, otherObjectPosition);

            if (distanceBetween <= affectDistance)
            {
                Destroy(affectedObject);
            }
        }

        Destroy(self);
    }

    public void Release()
    {
        GetComponent<Rigidbody>().useGravity = true;

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCast>());
        Destroy(GetComponent<LineRenderer>());

        Destroy(GameObject.Find("ProjectionIcon"));
    }
}

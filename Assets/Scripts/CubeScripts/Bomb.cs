using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] private float affectDistance;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject targetLocation;
    private GameObject target;

    private void Start()
    {
        target = Instantiate(targetLocation, new Vector3(-0.75f, 0.71f, 0), Quaternion.identity);
        ProjectionSphere scale = target.GetComponent<ProjectionSphere>();
        scale.SetParentObject(GetComponent<CubeRayCast>());
        scale.SetRadius(affectDistance);

    }

    public CubeData.CubeMaterialType GetCubeMaterialType()
    {
        return CubeData.CubeMaterialType.BOMB;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Blast();
        Platform.CallCubeLandedEvent();
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
                affectedObject.GetComponent<DynamicSpawnDestroyAnimation>().PlayDestroyAnimation();
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(self);
    }

    public void Release()
    {
        Destroy(target);
        GetComponent<Rigidbody>().useGravity = true;

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCast>());
        Destroy(GetComponent<LineRenderer>());
    }
}

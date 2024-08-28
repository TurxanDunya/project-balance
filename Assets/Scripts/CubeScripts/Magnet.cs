using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float attractDistance;
    [SerializeField] private float attractForce;
    [SerializeField] private ParticleSystem[] magnetEffect;
    [SerializeField] private GameObject selfPrefab;
    [SerializeField] private GameObject targetLocation;

    private GameObject target;

    private List<GameObject> attractibleObjects;

    private void Start()
    {
        target = Instantiate(targetLocation, new Vector3(-0.75f, 0.71f, 0), Quaternion.identity);

        ProjectionSphere scale = target.GetComponent<ProjectionSphere>();
        scale.SetParentObject(GetComponent<CubeRayCast>());
        scale.SetRadius(attractDistance);

        UpdateAttractibleObjectList();
    }

    private void OnEnable()
    {
        CubeFallDetector.magnetDetect += DestroySelfPrefab;
        Platform.CubeLanded += UpdateAttractibleObjectList;
    }

    private void OnDisable()
    {
        CubeFallDetector.magnetDetect -= DestroySelfPrefab;
        Platform.CubeLanded -= UpdateAttractibleObjectList;
    }

    private void UpdateAttractibleObjectList()
    {
        attractibleObjects = new List<GameObject>(
           GameObject.FindGameObjectsWithTag(TagConstants.DROPPED_CUBE));
    }    

    private void FixedUpdate()
    {
        foreach (GameObject attractibleObject in attractibleObjects)
        {
            Rigidbody rb = attractibleObject.GetComponent<Rigidbody>();

            Vector3 magnetPosition = transform.position;
            Vector3 otherObjectPosition = attractibleObject.transform.position;
            float distanceBetween = Vector3.Distance(magnetPosition, otherObjectPosition);

            if (distanceBetween <= attractDistance)
            {
                Vector3 direction = (magnetPosition - otherObjectPosition).normalized;
                rb.AddForce(attractForce * Time.deltaTime * direction, ForceMode.Acceleration);
            }
        }
    }

    public CubeData.CubeMaterialType GetCubeMaterialType()
    {
        return CubeData.CubeMaterialType.MAGNET;
    }

    public void Release()
    {
        Destroy(target);
        GetComponent<Rigidbody>().useGravity = true;

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCast>());
        Destroy(GetComponent<LineRenderer>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var effect in magnetEffect)
        {
            effect.Play();
        }

        RemoveLandedCubeFromAttractibles(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        AddLandedCubeToAttractibles(collision.gameObject);
    }

    private void AddLandedCubeToAttractibles(GameObject newLandedCube)
    {
        attractibleObjects.Add(newLandedCube);
    }

    private void RemoveLandedCubeFromAttractibles(GameObject newLandedCube)
    {
        attractibleObjects.Remove(newLandedCube);
    }

    private void DestroySelfPrefab()
    {
        Destroy(selfPrefab);
    }

}

using UnityEngine;

public class Resource : MonoBehaviour
{
    [Header("Resource Properties:")]
    public float regrowthTime = 5f;
    public bool isHarvested = false;
    public bool regrowthInProgress = false;

    [Header("Resource Drops:")]
    public string resourceDroppedName;
    public float minDropSpeedX = 0.1f;
    public float minDropSpeedY = 1f;
    public float minDropSpeedZ = 0.1f;
    public float maxDropSpeedX = 3f;
    public float maxDropSpeedY = 7f;
    public float maxDropSpeedZ = 3f;

    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Drop resource from pool
    public void DropResources()
    {
        GameObject drop = objectPooler.SpawnFromPool(resourceDroppedName, transform.position, Quaternion.identity);
        Rigidbody rb = drop.GetComponent<Rigidbody>();

        drop.transform.position = gameObject.transform.position;

        float dropXVelocity = Random.Range(minDropSpeedX, maxDropSpeedX);
        float dropYVelocity = Random.Range(minDropSpeedY, maxDropSpeedY);
        float dropZVelocity = Random.Range(minDropSpeedZ, maxDropSpeedZ);
        rb.velocity = new Vector3(dropXVelocity, dropYVelocity, dropZVelocity);
    }
}

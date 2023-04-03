using System.Collections;
using UnityEngine;

public class ResourceDrop : MonoBehaviour
{
    [SerializeField] private string resourceName;
    [SerializeField] private float absorptionDelay;
    [SerializeField] private float absorbSpeed;

    private ResourceScore resourceScore;
    private ObjectPooler objectPooler;
    private GameObject player;
    private bool isAbsorbable = false;
    private bool isMovable = false;

    private void Awake()
    {
        RetrieveComponents();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayAbsorption());
    }

    private void Update()
    {
        if (isMovable)
        {
            AbsorbResource();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && isMovable && isAbsorbable)
        {
            AbsorbResource();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && isAbsorbable)
        {
            resourceScore.AddPoints(resourceName, 1);

            gameObject.SetActive(false);
            objectPooler.ReturnToPool("TreeDrop", gameObject);

            isMovable = false;
            isAbsorbable = false;
        }
    }

    private IEnumerator DelayAbsorption()
    {
        yield return new WaitForSeconds(absorptionDelay);
        isMovable = true;
        isAbsorbable = true;
    }

    private void AbsorbResource()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 targetDirection = playerPosition - transform.position;

        transform.position += targetDirection * absorbSpeed * Time.deltaTime;
    }

    // Retrieve required components
    private void RetrieveComponents()
    {
        resourceScore = FindObjectOfType<ResourceScore>();
        objectPooler = FindObjectOfType<ObjectPooler>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
}

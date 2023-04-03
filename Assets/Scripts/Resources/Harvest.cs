using UnityEngine;
using UnityEngine.Pool;

public class Harvest : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        RetrieveComponents();
    }

    // Check collision object and perform action
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Resource")
        {
            Resource resource = collision.collider.GetComponent<Resource>();

            if (!resource.isHarvested)
            {
                HarvestResource(resource);
                resource.DropResources();
            }
        }
    }

    // Harvest the resource
    private void HarvestResource(Resource harvestResource)
    {
        harvestResource.isHarvested = true;
        harvestResource.gameObject.SetActive(false);
        gameManager.resourcesToGrowBack.Add(harvestResource);
    }

    // Retrieve required components
    private void RetrieveComponents()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
}

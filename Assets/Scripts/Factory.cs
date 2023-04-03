using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [Header("Resource Drop Physics:")]
    [SerializeField] private Vector3 dropOffset;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float throwAngle = 45f;

    [Header("Resource Drop Components:")]
    [SerializeField] private GameObject factoryResourcePrefab;
    [SerializeField] private Material woodMaterial;
    [SerializeField] private Material rockMaterial;

    private ResourceScore resourceScore;
    private ObjectPooler objectPooler;
    private GameObject player;

    private void Awake()
    {
        RetrieveComponents();
    }

    // What happens when player enters the range of factory
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            DetermineResourceScore();
        }
    }

    // Determine the scores of resources
    private void DetermineResourceScore()
    {
        foreach (KeyValuePair<string, int> score in resourceScore.scoreDictionary)
        {
            string scoreKey = score.Key;
            int scoreValue = score.Value;

            if (scoreValue > 0)
            {
                if (scoreKey == "Wood")
                {
                    DropResources(woodMaterial, scoreValue);
                }
                else if (scoreKey == "Rock")
                {
                    DropResources(rockMaterial, scoreValue);
                }
            }
        }

        RemovePointsFromScore();
    }

    // Drop resources into the factory from player inventory
    private void DropResources(Material resourceMaterial, int resourceCount)
    {
        // Calculate the initial velocity components
        float horizontalVelocity = throwSpeed * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
        float verticalVelocity = throwSpeed * Mathf.Sin(throwAngle * Mathf.Deg2Rad);

        for (int i = 1; i <= resourceCount; i++)
        {
            GameObject drop = objectPooler.SpawnFromPool("FactoryDrop", transform.position, Quaternion.identity);
            Rigidbody rb = drop.GetComponent<Rigidbody>();
            drop.GetComponent<MeshRenderer>().material = resourceMaterial;

            drop.transform.position = player.transform.position + dropOffset;

            // Apply the initial velocity to the object
            Vector3 throwDirection = transform.position - player.transform.position;
            Vector3 throwVelocity = (throwDirection * horizontalVelocity) + (Vector3.up * verticalVelocity);
            rb.velocity = throwVelocity;
        }
    }

    // Remove points from score for the amount of resources dropped
    private void RemovePointsFromScore()
    {

    }

    // Retrieve required components
    private void RetrieveComponents()
    {
        resourceScore = FindObjectOfType<ResourceScore>();
        objectPooler = FindObjectOfType<ObjectPooler>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
}

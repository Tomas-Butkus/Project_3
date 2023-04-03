using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Resource> resourcesToGrowBack;

    private void Update()
    {
        // Check if any resources need to regrow
        foreach (Resource resource in resourcesToGrowBack)
        {
            if (resource.isHarvested && !resource.regrowthInProgress)
            {
                StartCoroutine(StartRegrowth(resource));
            }
        }
    }

    private IEnumerator StartRegrowth(Resource resource)
    {
        resource.regrowthInProgress = true;

        yield return new WaitForSeconds(resource.regrowthTime);
        resource.gameObject.SetActive(true);
        resource.isHarvested = false;
        resource.regrowthInProgress = false;

        resourcesToGrowBack.Remove(resource);
    }
}

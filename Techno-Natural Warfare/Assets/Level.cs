using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<string> resourcesNameOnCompletion = null;
    [SerializeField] List<int> resourcesAmountOnCompletion = null;

    List<ResourceData> resourcesOnCompletion;
    Dictionary<Character, Vector3> characterToPosition = null; 

    void Start()
    {
        if (resourcesNameOnCompletion == null || resourcesAmountOnCompletion == null || resourcesNameOnCompletion.Count != resourcesNameOnCompletion.Count)
        {
            GenerateResources();
        }
        else
        {
            ConvertResourcesToResourceData();
        }
    }

    private void ConvertResourcesToResourceData()
    {
        resourcesOnCompletion = new List<ResourceData>();
        for (int i = 0; i < resourcesNameOnCompletion.Count; i++)
        {
            resourcesOnCompletion.Add(new ResourceData(resourcesNameOnCompletion[i], resourcesAmountOnCompletion[i]));
        }
    }

    private void GenerateResources()
    {
        resourcesOnCompletion = new List<ResourceData>()
        {
        new ResourceData("wood", 5),
        new ResourceData("scrap", 5),
        new ResourceData("plants", 5),
        new ResourceData("stone", 5),
        new ResourceData("oil", 5)
        };
    }

    void Update()
    {

    }


}

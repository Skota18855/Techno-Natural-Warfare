using System;

[System.Serializable]
public class ResourceData
{
    string resourceName;

    int resourceAmount;

    public ResourceData(string resourceName, int resourceAmount)
    {
        ResourceName = resourceName;
        ResourceAmount = resourceAmount;
    }

    public string ResourceName { get => resourceName; set => resourceName = value; }
    public int ResourceAmount { get => resourceAmount; set => resourceAmount = value; }
}
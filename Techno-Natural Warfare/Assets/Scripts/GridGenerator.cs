using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject prefabOfObject;

    [SerializeField] Vector3 gridSize;

    [SerializeField] bool isHollow;

    void Start()
    {
        for (int x = 0; x < gridSize.x; ++x)
        {
            for (int y = 0; y < gridSize.y; ++y)
            {
                for (int z = 0; z < gridSize.z; ++z)
                {
                    if (isHollow &&
                        x > 0 && x < gridSize.x - 1 &&
                        y > 0 && y < gridSize.y - 1 &&
                        z > 0 && z < gridSize.z - 1)
                        continue;

                    GameObject go = Instantiate(prefabOfObject, new Vector3(x, y, z), Quaternion.identity, GetComponent<Transform>());
                    go.name = $"Position {x},{y},{z}";
                }
            }
        }

    }
}

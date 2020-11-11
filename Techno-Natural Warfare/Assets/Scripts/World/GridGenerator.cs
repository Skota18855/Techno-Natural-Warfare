using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject prefabOfObject;
    [SerializeField] Vector3 gridSize;
    [SerializeField] bool isHollow;

    List<TacticsMove> units;

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        units = FindObjectsOfType<TacticsMove>().ToList();
        if(units.GroupBy(units=>units.startingTile).Count() != units.Count)
        {
            Debug.LogError("Some units have the same starting tile.");
            return;
        }
        transform.position = Vector3.zero;
        var allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform item in allChildren)
        {
            if (item.gameObject != this.gameObject)
            {
                DestroyImmediate(item.gameObject);
            }
        }

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

                    GameObject go = Instantiate(prefabOfObject, new Vector3(x, y, z), Quaternion.identity, transform);
                    go.name = $"Position {x},{y},{z}";
                    go.AddComponent(typeof(Tile));
                    go.tag = "Tile";
                    foreach (TacticsMove tm in units)
                    {
                        if (tm.startingTile.Equals(new Vector3(x, y, z)))
                        {
                            Tile tile = go.GetComponent<Tile>();
                            tm.currentTile = tile;
                            tile.occupyingUnit = tm;

                            tm.transform.position = new Vector3(x,y+1+(tm.transform.localScale.y/2),z);
                            tm.transform.parent = go.transform;
                        }
                    }
                }
            }
        }
        transform.position = new Vector3(-(gridSize.x - 1) / 2, (gridSize.y - 1) / 2, -(gridSize.z - 1) / 2);

        foreach (TacticsMove tm in units)
        {
            tm.transform.parent = null;
        }

        Camera.main.transform.position = new Vector3(0, Math.Max(gridSize.x, gridSize.z), 0);
    }
}

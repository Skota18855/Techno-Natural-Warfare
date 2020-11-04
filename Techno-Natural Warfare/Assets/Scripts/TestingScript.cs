using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    private void Start()
    {
        var a = GetComponentsInChildren<Transform>();
        var b = a.Where(transform => transform.gameObject.name == "Panel 1");
        var c = b.FirstOrDefault();
        GameObject gameObject = c.gameObject;
        if (gameObject != null)
        {
            Debug.Log("Found Panel 1 when not active");
        }
    }
}

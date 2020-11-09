using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private Character character;

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeCellMaterial(Material materialToChangeTo)
    {
        GetComponent<MeshRenderer>().material = materialToChangeTo;
    }
}

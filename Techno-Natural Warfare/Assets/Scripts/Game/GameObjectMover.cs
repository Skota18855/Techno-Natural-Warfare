using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMover : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] Vector3 amountToMove;
    
    [SerializeField] float movementInterval;

    [SerializeField] Vector3 positionToStopAt;

    float timePassed;
    
    // Start is called before the first frame update
    void Start()
    {
        timePassed = movementInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed -= Time.deltaTime;
        
        if(timePassed <= 0)
        {
            timePassed = movementInterval;
            
            objectToMove.position = 
                new Vector3(
                    (Mathf.Abs(objectToMove.position.x - positionToStopAt.x) > 0.5) ? (objectToMove.position.x + amountToMove.x) : (objectToMove.position.x), 
                    (Mathf.Abs(objectToMove.position.y - positionToStopAt.y) > 0.5) ? (objectToMove.position.y + amountToMove.y) : (objectToMove.position.y), 
                    (Mathf.Abs(objectToMove.position.z - positionToStopAt.z) > 0.5) ? (objectToMove.position.z + amountToMove.z) : (objectToMove.position.z));
        }
    }
}

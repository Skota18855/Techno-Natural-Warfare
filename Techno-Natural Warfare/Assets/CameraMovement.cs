using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float amountToMove = (float)0.01;
    // Update is called once per frame
    void Update()
    {
        if (Game.Instance != null)
        {
            if (Input.GetKey(Game.Instance.Data.Settings.MoveCameraUp))
            {
                transform.position += new Vector3(0, 0, (float)amountToMove);
            }

            if (Input.GetKey(Game.Instance.Data.Settings.MoveCameraDown))
            {
                transform.position += new Vector3(0, 0, (float)-amountToMove);
            }

            if (Input.GetKey(Game.Instance.Data.Settings.MoveCameraLeft))
            {
                transform.position += new Vector3((float)-amountToMove, 0, 0);
            }

            if (Input.GetKey(Game.Instance.Data.Settings.MoveCameraRight))
            {
                transform.position += new Vector3((float)amountToMove, 0, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, (float)amountToMove);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, 0, (float)-amountToMove);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3((float)-amountToMove, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3((float)amountToMove, 0, 0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            if (!hasMoved)
            {
                FindSelectableTiles();
                CheckMouse();
            }
            else
            {

            }
        }
        else
        {
            Move();
        }

        if (actions == 0 && !moving)
        {
            TurnManager.EndTurn();
        }
    }

    void CheckMouse()
    {
        if ((Game.Instance != null) ? (Input.GetKeyUp(Game.Instance.Data.Settings.SelectCharacter)) : (Input.GetKeyUp(KeyCode.Mouse1)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (!hasMoved)
                    {
                        if (t.selectableWith1Point || t.selectableWith2Points && (t.occupyingUnit == null || t.occupyingUnit == this))
                        {
                            MoveToTile(t);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}

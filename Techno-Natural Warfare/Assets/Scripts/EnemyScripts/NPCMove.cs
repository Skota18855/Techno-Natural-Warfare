using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;

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

        if (!moving && actions > 0)
        {
            FindNearestTarget();
            if (!hasMoved)
            {
                CalculatePath();
                FindSelectableTiles();
                actualTargetTile.target = true;
            }
            else
            {
                if (Physics.OverlapSphere(transform.position, equippedWeapon.AttackRange).ToList().Contains(target.GetComponent<Collider>()))
                {
                    AttackTarget(target.GetComponent<TacticsMove>());
                }
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

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}

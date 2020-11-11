using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static List<TacticsMove> turnTeam = new List<TacticsMove>();

    static TacticsMove SelectedUnit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurn();
        }

        if ((Game.Instance != null) ? (Input.GetKeyUp(Game.Instance.Data.Settings.SelectTile)) : (Input.GetKeyUp(KeyCode.Mouse0)) && SelectedUnit == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if (t.occupyingUnit != null && t.occupyingUnit.actions != 0)
                    {
                        SelectedUnit = t.occupyingUnit;
                        SelectedUnit.BeginTurn();
                    }
                }
            }
        }
    }

    static void InitTeamTurn()
    {
        List<TacticsMove> teamList = units[turnKey.Peek()];

        foreach (TacticsMove unit in teamList)
        {
            turnTeam.Add(unit);
            unit.actions = 2;
            unit.hasMoved = false;
        }

        if (turnKey.Peek() != "Player")
        {
            SelectedUnit = turnTeam[0];
            turnTeam[0].BeginTurn();
        }
    }

    public static void StartTurn()
    {
        SelectedUnit.BeginTurn();
    }

    public static void EndTurn()
    {
        turnTeam.Remove(SelectedUnit);
        SelectedUnit.EndTurn();
        SelectedUnit = (turnKey.Peek() == "Player" || turnTeam.Count < 1) ? (null) : (turnTeam[0]);
        if (turnTeam.Count < 1)
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurn();
        }
    }

    public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }

    public void AtackButtonClicked()
    {
        if (SelectedUnit != null && !SelectedUnit.moving && SelectedUnit.tag == "Player")
        {
            List<Collider> attackableTiles = Physics.OverlapSphere(SelectedUnit.transform.position, SelectedUnit.equippedWeapon.AttackRange).ToList().Where(collider => collider.gameObject.GetComponent<Tile>() != null && collider.gameObject.GetComponent<Tile>().occupyingUnit != null && !turnTeam.Contains(collider.gameObject.GetComponent<Tile>().occupyingUnit)).ToList();
            attackableTiles.ForEach(
                collider => collider.gameObject.GetComponent<Tile>().attackable = true
            );
            StartCoroutine("ChoosingTarget", attackableTiles);
        }
    }

    public void WaitButtonClicked()
    {
        if (SelectedUnit != null && !SelectedUnit.moving && SelectedUnit.tag == "Player")
        {
            EndTurn();
        }
    }

    IEnumerator ChoosingTarget(List<Collider> attackableTiles)
    {
        TacticsMove target = null;
        while (target == null)
        {
            if ((Game.Instance != null) ? (Input.GetKeyUp(Game.Instance.Data.Settings.SelectTile)) : (Input.GetKeyUp(KeyCode.Mouse1)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        Tile t = hit.collider.GetComponent<Tile>();

                        if (t.occupyingUnit != null && !turnTeam.Contains(t.occupyingUnit) && t.attackable)
                        {
                            target = t.occupyingUnit;
                        }
                    }
                }
            }

            yield return null;
        }

        attackableTiles.ForEach(collider => collider.gameObject.GetComponent<Tile>().Reset());
        StopCoroutine("ChoosingTarget");
        SelectedUnit.AttackTarget(target, false);
        yield return null;
    }
}

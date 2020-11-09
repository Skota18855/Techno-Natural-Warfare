using System.Collections;
using System.Collections.Generic;
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
            InitTeamTurnQueue();
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

    static void InitTeamTurnQueue()
    {
        List<TacticsMove> teamList = units[turnKey.Peek()];

        foreach (TacticsMove unit in teamList)
        {
            turnTeam.Add(unit);
        }

        if(turnKey.Peek() != "Player")
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
        SelectedUnit = (turnKey.Peek() == "Player" || turnTeam.Count < 1)?(null):(turnTeam[0]);
        if (turnTeam.Count < 1)
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
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

    public static void AtackButtonClicked()
    {

    }

    public static void WaitButtonClicked()
    {
        if(SelectedUnit != null) EndTurn();
    }
}

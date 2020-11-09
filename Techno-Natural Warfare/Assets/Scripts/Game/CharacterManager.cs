using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private List<Character> allCharacters = new List<Character>();
    private Queue<Character> charactersInOrder = new Queue<Character>();

    private Character selectedCharacter = null;

    // Update is called once per frame
    void Update()
    {
        //if (!Game.Instance.Enemy.EnemyCharacters.Contains(charactersInOrder.Peek()))
        //{
        //    if (Input.GetKeyDown(Game.Instance.Data.Settings.Select))
        //    {
        //        RaycastHit hit;
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            Transform objectHit = hit.transform;

        //            if (objectHit.gameObject.tag == "Cell" && charactersInOrder.Peek() == objectHit.gameObject.GetComponent<GridCell>().Character)
        //            {
        //                selectedCharacter = charactersInOrder.Dequeue();
        //                ShowCharactersRange();
        //            }
        //        }
        //    }
        //}
        //else
        //{

        //}
    }

    private void ShowCharactersRange()
    {
        throw new NotImplementedException();
    }

    public void GetAllCharacters()
    {
        allCharacters = new List<Character>();
        allCharacters.AddRange(Game.Instance.Player.PlayerCharacters);
        allCharacters.AddRange(Game.Instance.Enemy.EnemyCharacters);
        DetermineOrder();
    }

    private void DetermineOrder()
    {
        allCharacters.OrderBy(Character => Character.Speed);
        foreach (Character character in allCharacters)
        {
            charactersInOrder.Enqueue(character);
        }

    }
}

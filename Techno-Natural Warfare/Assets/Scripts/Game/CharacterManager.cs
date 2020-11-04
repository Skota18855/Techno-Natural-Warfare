using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private List<Character> allCharacters = new List<Character>();
    private Queue<Character> charactersInOrder = new Queue<Character>();

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAllCharacters()
    {
        allCharacters = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static bool isPaused = false;

    [SerializeField] Canvas pauseMenu = null;
    [SerializeField] Canvas ConfirmPopup = null;

    bool choice = false;
    public bool Choice
    {
        get
        {
            return choice;
        }
        set
        {
            choice = value;
            ConfirmPopup.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f;
        isPaused = false;
    }

    public void ExitLevel()
    {
        ConfirmPopup.enabled = true;
        StartCoroutine("PromptForLevelExit");
    }

    IEnumerator PromptForLevelExit()
    {
        while (ConfirmPopup.enabled)
        {
            yield return null;
        }
        StopCoroutine("PromptForLevelExit");

        if (choice)
        {
            ResumeGame();
            Game.Instance.SceneManagerObject.LoadSceneAsyncByName("Level Select");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scene Manager Object", menuName = "Game/Scene Manager Object")]
public class SceneManagerObject : ScriptableObject
{
    [SerializeField] List<string> sceneNames = null;
    public void LoadSceneAsyncByName(string sceneName)
    {
        if (sceneNames.Contains(sceneName))
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            Debug.LogError($"Scriptable Object doesn't contain scene name {sceneName}");
        }
    }

    public void LoadSceneAsyncByIndex(int index)
    {
        if (sceneNames.Count != 0 && index >= 0 && index < sceneNames.Count)
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        }
        else
        {
            if(sceneNames.Count ==0)
            {
                Debug.LogError("Add the Scene names to the scriptable object.");
            }
            else
            {
                Debug.LogError($"Make sure that the index is within the range of 0 to {sceneNames.Count}.");
            }
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        if (sceneNames.Contains(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scriptable Object doesn't contain scene name {sceneName}");
        }
    }

    public void LoadSceneByIndex(int index)
    {
        if (sceneNames.Count != 0 && index >= 0 && index < sceneNames.Count)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            if (sceneNames.Count == 0)
            {
                Debug.LogError("Add the Scene names to the scriptable object.");
            }
            else
            {
                Debug.LogError($"Make sure that the index is within the range of 0 to {sceneNames.Count}.");
            }
        }
    }

    public Scene CurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }
}

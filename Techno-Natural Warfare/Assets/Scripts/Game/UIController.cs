using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject achievementPanelPrefab = null;
    private Dictionary<string, GameObject> nameToPanels = new Dictionary<string, GameObject>();

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        nameToPanels = new Dictionary<string, GameObject>();
        List<Canvas> allCanvases = FindObjectsOfType<Canvas>().ToList();

        foreach (Canvas canvas in allCanvases)
        {
            CanvasScaler cScaler = canvas.GetComponent<CanvasScaler>();
            cScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cScaler.referenceResolution = new Vector2(Screen.width, Screen.height);

            foreach (RectTransform rt in canvas.GetComponentsInChildren<RectTransform>().Where(panel => panel.tag == "Panel"))
            {
                GameObject go = rt.gameObject;
                nameToPanels.Add(go.name, go);

                go.SetActive(false);
            }
        }

    }

    public void ShowPanel(string panelName, string prompt)
    {
        if (nameToPanels.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(true);
            panel.GetComponentsInChildren<TextMeshProUGUI>().Where(text => text.tag == "Prompt").FirstOrDefault().text = prompt;
        }
        else
        {
            Debug.LogError($"Panel with name \"{panelName}\" does not exist in this scene.");
        }
    }

    public void ChangeAllPanelsIsActive(bool isActive)
    {
        foreach (KeyValuePair<string, GameObject> item in nameToPanels)
        {
            item.Value.SetActive(isActive);
        }
    }

    public void HidePanel(string panelName)
    {
        if (nameToPanels.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError($"Panel with name \"{panelName}\" does not exist in this scene.");
        }
    }

    public void ShowAchievement(Achievement achievement)
    {
        GameObject achievementPanel = GameObject.Instantiate(achievementPanelPrefab, GetMainCanvas().transform);
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/Achievements/");

        //achievementPanel.GetComponent<RectTransform>().position = Vector3.zero;

        achievementPanel.GetComponentsInChildren<Image>().Where(image => image.name == "AchievementImage").FirstOrDefault().sprite = sprites.Where(sprite => sprite.name == achievement.Name).FirstOrDefault();
        achievementPanel.GetComponentInChildren<Text>().text = achievement.Name;

        StartCoroutine("ShowAchievementCoroutine", achievementPanel);
    }

    public Canvas GetMainCanvas()
    {
        Canvas main = GetComponentInChildren<Canvas>();

        return main;
    }

    IEnumerator ShowAchievementCoroutine(GameObject achievementPanel)
    {
        StopCoroutine("ShowAchievement");
        WaitForSeconds waitForFive = new WaitForSeconds(7);
        yield return waitForFive;
        Destroy(achievementPanel);
        yield return null;
    }
}

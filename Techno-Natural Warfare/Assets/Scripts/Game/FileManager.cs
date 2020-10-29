using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class FileInfo
{
    public string[] names;
    public int[] achievements;
    public int[] playTimes;
    public bool[] isNews;

    public FileInfo(string[] names, int[] achievements, int[] playTimes, bool[] isNews)
    {
        this.names = names;
        this.achievements = achievements;
        this.playTimes = playTimes;
        this.isNews = isNews;
    }

    public FileInfo()
    {
        this.names = new string[FileManager.NUM_OF_FILES];
        this.achievements = new int[FileManager.NUM_OF_FILES];
        this.playTimes = new int[FileManager.NUM_OF_FILES];
        this.isNews = new bool[FileManager.NUM_OF_FILES];

        for (int i = 0; i < FileManager.NUM_OF_FILES; i++)
        {
            names[i] = "New Player";
            achievements[i] = 0;
            playTimes[i] = 0;
            isNews[i] = true;
        }
    }
}
public class FileManager : MonoBehaviour
{
    public const int NUM_OF_FILES = 3;

    private int selectedFile = -1;

    public int SelectedFile
    {
        get { return selectedFile; }
        set { selectedFile = value; }
    }

    private int copyFromFile = -1;

    public int CopyFromFile
    {
        get { return copyFromFile; }
        set
        {
            copyFromFile = value;
            fileSelectionInfo.text = "Please select the file to copy to";
        }
    }

    private int copyToFile = -1;

    public int CopyToFile
    {
        get { return copyToFile; }
        set { copyToFile = value; StartCoroutine("PromptForCopy"); }
    }

    FileInfo info;

    private TextMeshProUGUI fileSelectionInfo;
    private TextMeshProUGUI nameInfo;
    private GameObject[] files;
    private Button copyButton;
    private Button deleteButton;    
    private Button yesButton;
    private Button noButton;
    private Button confirmButton;
    TMP_InputField inputField;

    private bool isDeleting = false;

    public bool IsDeleting
    {
        get { return isDeleting; }
        set
        {
            isDeleting = value;
            isCopying = false;
            copyFromFile = -1;
            copyToFile = -1;
        }
    }

    private bool isCopying = false;

    public bool IsCopying
    {
        get { return isCopying; }
        set
        {
            isCopying = value;
            isDeleting = false;
            copyFromFile = -1;
            copyToFile = -1;
        }
    }

    private bool? choice = null;

    public bool? Choice
    {
        get { return choice; }
        set { choice = value; }
    }

    private bool isCharacterNamed = false;

    public bool IsCharacterNamed
    {
        get { return isCharacterNamed; }
        set { isCharacterNamed = value; }
    }

    private string characterName;

    public string CharacterName
    {
        get
        {
            return characterName;
        }
        set
        {
            characterName = value;
            IsCharacterNamed = true;
        }
    }

    public FileInfo Info { get => info; set => info = value; }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        if (loadedScene.name == "File Select Menu")
        {
            SelectedFile = -1;

            Game.Instance.UIController.ChangeAllPanelsIsActive(true);
            FindNeededResources();
            UpdateGUI();
            SetupButtons();
            Game.Instance.UIController.ChangeAllPanelsIsActive(false);
            Game.Instance.UIController.ShowPanel("MainPanel", "Please select a file to play");
        }

        if (selectedFile != -1)
        {
            SaveSystem.SaveObject<GameData>(Game.Instance.Data, $"file{SelectedFile}gamedata.gme");

            SaveSystem.SaveObject<FileInfo>(info, "fileInfo.info");
        }
    }

    private void UpdateGUI()
    {
        for (int i = 0; i < NUM_OF_FILES; i++)
        {
            GameObject file = files[i];
            file.GetComponentsInChildren<TextMeshProUGUI>().Where(text => text.name == "Player Name").FirstOrDefault().text = $"Name\n{Info.names[i]}";

            TimeSpan timeSpan = TimeSpan.FromSeconds(info.playTimes[i]);
            int totalMins = timeSpan.Minutes;
            int totalHrs = (int)timeSpan.TotalHours;

            file.GetComponentsInChildren<TextMeshProUGUI>().Where(text => text.name == "Play Time").FirstOrDefault().text = $"Playtime\n{totalHrs}h {totalMins}m";
            file.GetComponentsInChildren<TextMeshProUGUI>().Where(text => text.name == "Achievements").FirstOrDefault().text = $"Achievements\n{Info.achievements[i]}";
        }

        IsCopying = false;
        IsDeleting = false;

        copyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Copy";
        deleteButton.GetComponentInChildren<TextMeshProUGUI>().text = "Delete";

        fileSelectionInfo.text = "Please select a file";
        fileSelectionInfo.text = "Please select a file";

        copyToFile = -1;
        copyFromFile = -1;
    }

    public void FindNeededResources()
    {
        fileSelectionInfo = GameObject.FindObjectsOfType<TextMeshProUGUI>().Where(textMeshPro => textMeshPro.name == "File Selection Info").FirstOrDefault();
        nameInfo = GameObject.FindObjectsOfType<TextMeshProUGUI>().Where(text => text.name == "Name Info").FirstOrDefault();

        files = GameObject.FindGameObjectsWithTag("File");
        copyButton = GameObject.FindObjectsOfType<Button>().Where(button => button.name == "Copy Button").FirstOrDefault();
        deleteButton = GameObject.FindObjectsOfType<Button>().Where(button => button.name == "Delete Button").FirstOrDefault();

        yesButton = GameObject.FindObjectsOfType<Button>().Where(button => button.name == "Yes Button").FirstOrDefault();
        noButton = GameObject.FindObjectsOfType<Button>().Where(button => button.name == "No Button").FirstOrDefault();

        inputField = GameObject.FindObjectsOfType<TMP_InputField>().Where(input => input.name == "Name Field").FirstOrDefault();
        confirmButton = GameObject.FindObjectsOfType<Button>().Where(button => button.name == "Confirm Button").FirstOrDefault();

        Info = SaveSystem.LoadObject<FileInfo>("fileinfo.info");
    }



    public void SetupButtons()
    {
        for (int i = 0; i < NUM_OF_FILES; i++)
        {
            int fileNumber = i;
            var a = files[i].GetComponentsInChildren<Button>().Where(btn => btn.name == "Button").FirstOrDefault();
            a.onClick.AddListener(delegate { FileClicked(fileNumber + 1); });
        }

        deleteButton.onClick.AddListener(delegate { DeleteButtonClicked(); });
        copyButton.onClick.AddListener(delegate { CopyButtonClicked(); });

        yesButton.onClick.AddListener(delegate { Choice = true; }); 
        noButton.onClick.AddListener(delegate { Choice = false; });

        confirmButton.onClick.AddListener(delegate { NameCharacterFromInput(inputField); });
    }

    private void CopyButtonClicked()
    {
        IsCopying = !IsCopying;
        copyButton.GetComponentInChildren<TextMeshProUGUI>().text = ((IsCopying) ? ("Cancel") : ("Copy"));
        deleteButton.GetComponentInChildren<TextMeshProUGUI>().text = "Delete";
        fileSelectionInfo.text = ((IsCopying) ? ("Please select the file to copy from") : ("Please select a file"));
    }

    private void DeleteButtonClicked()
    {
        IsDeleting = !IsDeleting;
        deleteButton.GetComponentInChildren<TextMeshProUGUI>().text = ((IsDeleting) ? ("Cancel") : ("Delete"));
        copyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Copy";
        fileSelectionInfo.text = ((IsDeleting) ? ("Please select the file to delete") : ("Please select a file"));
    }

    private void FileClicked(int fileClicked)
    {
        if (IsCopying)
        {
            if (copyFromFile == -1 || copyFromFile == fileClicked)
            {
                CopyFromFile = fileClicked;
            }
            else
            {
                CopyToFile = fileClicked;
            }
        }
        else if (IsDeleting)
        {
            StartCoroutine("PromptForDelete", fileClicked);
        }
        else
        {
            if (Info.isNews[fileClicked])
            {
                StartCoroutine("PromptForName", fileClicked);
            }
        }
    }

    private void DeleteFile(int deletingFile)
    {
        info.names[deletingFile-1] = "New Player";
        info.playTimes[deletingFile-1] = 0;
        info.achievements[deletingFile-1] = 0;
        info.isNews[deletingFile-1] = true;

        UpdateGUI();
    }

    IEnumerator PromptForDelete(int fileIndex)
    {
        Game.Instance.UIController.ShowPanel("YesNoPanel", $"Are you sure you wish to delete file {fileIndex}?");

        while (Choice == null)
        {
            yield return null;
        }

        StopCoroutine("PromptForDelete");

        if (choice == true)
        {
            DeleteFile(fileIndex);
        }

        Choice = null;
        Game.Instance.UIController.HidePanel("YesNoPanel");
    }

    IEnumerator PromptForCopy()
    {
        Game.Instance.UIController.ShowPanel("YesNoPanel", $"Are you sure you wish to copy file {CopyFromFile} to file {CopyToFile}?");

        while (Choice == null)
        {
            yield return null;
        }

        StopCoroutine("PromptForCopy");

        if (choice == true)
        {
            CopyFiles();
        }

        Choice = null;
        Game.Instance.UIController.HidePanel("YesNoPanel");
    }

    private void CopyFiles()
    {
        info.names[copyToFile-1] = info.names[copyFromFile-1];
        info.playTimes[copyToFile-1] = info.playTimes[copyFromFile-1];
        info.achievements[copyToFile-1] = info.achievements[copyFromFile-1];
        info.isNews[copyToFile-1] = info.isNews[copyFromFile-1];

        UpdateGUI();
    }

    private IEnumerator PromptForName(int fileIndex)
    {
        Game.Instance.UIController.ShowPanel("NamePanel", $"Please enter your name?");

        while (!IsCharacterNamed)
        {
            yield return null;
        }
        StopCoroutine("NameCharacter");
        
        info.names[fileIndex-1] = CharacterName;
        info.isNews[fileIndex-1] = false;

        GameData data = SaveSystem.LoadObject<GameData>($"file{fileIndex}gamedata.gme");
        data.PlayerName = CharacterName;
        Game.Instance.Data = data;

        SelectedFile = fileIndex;
        Game.Instance.SceneManagerObject.LoadSceneAsyncByName("Main Menu");
        yield return null;
    }
    public void NameCharacterFromInput(TMP_InputField inputField)
    {
        if (inputField.text.Trim().Length != 0)
        {
            CharacterName = inputField.text;
        }
        else
        {
            nameInfo.text = $"Please enter a valid character name.\n(Can't be empty)";
        }
    }
}

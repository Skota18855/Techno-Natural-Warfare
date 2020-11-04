using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    public const int NUM_OF_ACHIEVEMENTS = 10;

    private static Game instance;

    public static Game Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private GameData data;

    public GameData Data
    {
        get { return data; }
        set { data = value; }
    }

    public SceneManagerObject SceneManagerObject;

    private UIController uiController;

    public UIController UIController
    {
        get { return  uiController; }
        set {  uiController = value; }
    }

    private FileManager fileManager;

    public FileManager FileManager
    {
        get { return fileManager; }
        set { fileManager = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Game.Instance = this;
            GameData data = new GameData();

            UIController = GetComponent<UIController>();
            FileManager = GetComponent<FileManager>();

            Instance.Data = data;

            if (!File.Exists($"{Application.persistentDataPath}\\fileinfo.info"))
            {
                FileInfo info = new FileInfo();
                SaveSystem.SaveObject<FileInfo>(info, "fileinfo.info");
            }

            for (int i = 0; i < FileManager.NUM_OF_FILES; i++)
            {
                if (!File.Exists($"{Application.persistentDataPath}\\file{i + 1}gamedata.gme"))
                {
                    SaveSystem.SaveObject<GameData>(data, $"file{i + 1}gamedata.gme");
                }
            }
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

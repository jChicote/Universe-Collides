using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayerSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Settings")]
    public GameSettings gameSettings;

    [Header("Player Events")]
    public UnityEvent OnMainMenu;
    
    [Header("Loading Screen UI")]
    public GameObject LoadingScreen;

    [HideInInspector] public SessionData sessionData;
    [HideInInspector] public MainMenuUI mainMenuUI = null;
    [HideInInspector] public SceneController sceneController;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sessionData = this.gameObject.AddComponent<SessionData>();
        Instantiate(gameSettings.audioManagerPrefab, transform.position, Quaternion.identity);
        OnMainMenu.AddListener(ShowMainMenu);

        //Create scene loader and initialise.
        SceneLoader sceneLoader = this.GetComponent<SceneLoader>();
        sceneLoader.Init();
    }

    private void Start()
    {
        ShowMainMenu();
    }

    /// <summary>
    /// Reveals menu when called.
    /// </summary>
    public void ShowMainMenu() 
    {
        if(mainMenuUI != null) return;

        mainMenuUI = Instantiate(gameSettings.mainMenuPrefab, transform.position, Quaternion.identity).GetComponent<MainMenuUI>();
    }

    /// <summary>
    /// Destroys menu when called.
    /// </summary>
    public void CloseMainMenu()
    {
        mainMenuUI.OnClose();
    }
}

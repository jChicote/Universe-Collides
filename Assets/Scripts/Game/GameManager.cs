using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Settings")]
    public GameSettings gameSettings;

    [Header("Player Events")]
    public UnityEvent OnMainMenu;
    public UnityEvent OnGameplayStart;
    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnPlayerDeath;

    [HideInInspector] public SessionData sessionData;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Camera sceneCamera = null;
    [HideInInspector] public UIHudManager gameplayHUD = null;
    [HideInInspector] public MainMenuUI mainMenuUI = null;
    [HideInInspector] public ScoreManager scoreManager;
    public List<SpawnManager> spawnerManagers;

    public GameObject[] sceneEntities;

    void Awake() {
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
        OnGameplayStart.AddListener(BeginGamePlay);
        OnMainMenu.AddListener(ShowMainMenu);
    }

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu() {
        if(mainMenuUI != null) return;

        mainMenuUI = Instantiate(gameSettings.mainMenuPrefab, transform.position, Quaternion.identity).GetComponent<MainMenuUI>();
    }
    public void BeginGamePlay() {

        //Scene and UI is loaded first
        sceneCamera = GameObject.Instantiate(gameSettings.sceneCamera).GetComponent<Camera>();
        GameObject.Instantiate(gameSettings.postProcessingPrefab, transform.position, Quaternion.identity);

        AimUI aimSightUI = GameObject.Instantiate(gameSettings.UIaimSightPrefab, transform.position, Quaternion.identity).GetComponent<AimUI>();
        UIPointerManager pointerManagerUI = GameObject.Instantiate(gameSettings.UIPointerManagerPrefab, transform.position, Quaternion.identity).GetComponent<UIPointerManager>();
        pointerManagerUI.Init(sceneCamera);

        ScoreUI scoreUI = Instantiate(gameSettings.scoreUIPrefab, transform.position, Quaternion.identity).GetComponent<ScoreUI>();
        scoreUI.Init();
        scoreManager = Instantiate(gameSettings.scoreManagerPrefab, transform.position, Quaternion.identity).GetComponent<ScoreManager>();
        scoreManager.Init(scoreUI);

        gameplayHUD = GameObject.Instantiate(gameSettings.UIHudPrefab,transform.position,Quaternion.identity).GetComponent<UIHudManager>();
        gameplayHUD.Init(aimSightUI, pointerManagerUI, scoreUI);

        //Load Entities
        SpawnEntities();
    }

    public void SpawnEntities() {
        spawnerManagers = FindObjectsOfType<SpawnManager>().Where(x => x.GetComponent<ISpawner>() != null).ToList();
        Type spawnerType = gameSettings.battleSpawnModes.Where(x => x.mode == sessionData.battleMode).First().modeClassType;
        BattleModeSpawner modeSpawner = Activator.CreateInstance(spawnerType) as BattleModeSpawner;
        modeSpawner.Init(this, spawnerManagers[0], spawnerManagers[1]);
        modeSpawner.RunInstance();
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void LoadLevel(int sceneIndex) {
        StartCoroutine(ILoadScene(sceneIndex));
    }

    IEnumerator ILoadScene(int index) {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        OnGameplayStart.Invoke();
    }
}

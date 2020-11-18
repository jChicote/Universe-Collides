using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using PlayerSystems;

public class SceneController : MonoBehaviour
{
    public UnityEvent OnGameplayStart;
    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnPlayerDeath;

    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Camera sceneCamera = null;
    [HideInInspector] public UIHudManager gameplayHUD = null;
    [HideInInspector] public ScoreManager scoreManager;
    public List<SpawnManager> spawnerManagers;

    public List<Dictionary<TeamColor, GameObject>> sceneEntities;

    private GameSettings gameSettings;
    private SessionData sessionData;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.Instance.sceneController = this;
        gameSettings = GameManager.Instance.gameSettings;
        sessionData = GameManager.Instance.sessionData;
        OnGameplayStart.AddListener(PrepareScene);
    }

    /// <summary>
    /// Manages the sequential creation of important scene objects.
    /// </summary>
    private void PrepareScene()
    {
        LoadSceneCamera();
        LoadHUD();

        //Play Track
        AudioManager.Instance.onTrackEvent.Invoke(BackgroundOstType.StarWars);

        //Load Entities
        SpawnEntities();
    }

    /// <summary>
    /// Loads scene camera.
    /// </summary>
    private void LoadSceneCamera()
    {
        //Scene and UI is loaded first
        sceneCamera = GameObject.Instantiate(gameSettings.sceneCamera).GetComponent<Camera>();
        GameObject.Instantiate(gameSettings.postProcessingPrefab, transform.position, Quaternion.identity);

    }

    /// <summary>
    /// Loads game HUD UI.
    /// </summary>
    private void LoadHUD()
    {
        GameObject animatingCanvas = Instantiate(gameSettings.animatingCanvas, transform.position, Quaternion.identity) as GameObject;
        GameObject staticGameHUD = GameObject.Instantiate(gameSettings.UIHudPrefab, transform.position, Quaternion.identity) as GameObject;
        gameplayHUD = staticGameHUD.GetComponent<UIHudManager>();

        LoadUI(animatingCanvas, staticGameHUD);
    }

    /// <summary>
    /// Loads relevant scene UI Elements for gameplay.
    /// </summary>
    public void LoadUI(GameObject animatingCanvas, GameObject staticGameHUD)
    {
        AimUI aimSightUI = GameObject.Instantiate(gameSettings.UIaimSightPrefab, transform.position, Quaternion.identity).GetComponent<AimUI>();
        aimSightUI.Init();

        UIPointerManager pointerManagerUI = GameObject.Instantiate(gameSettings.UIPointerManagerPrefab, transform.position, Quaternion.identity).GetComponent<UIPointerManager>();
        pointerManagerUI.Init(sceneCamera);

        ScoreUI scoreUI = Instantiate(gameSettings.scoreUIPrefab, staticGameHUD.transform).GetComponent<ScoreUI>();
        scoreUI.Init();

        scoreManager = Instantiate(gameSettings.scoreManagerPrefab, transform.position, Quaternion.identity).GetComponent<ScoreManager>();
        scoreManager.Init(scoreUI);

        HealthBarUI healthBar = Instantiate(gameSettings.healthBarUIPrefab, staticGameHUD.transform).GetComponent<HealthBarUI>();
        ThrustUI thrustUI = Instantiate(gameSettings.thrustUIPrefab, staticGameHUD.transform).GetComponent<ThrustUI>();

        gameplayHUD.Init(aimSightUI, pointerManagerUI, scoreUI, healthBar, thrustUI);
    }

    /// <summary>
    /// Spawns the team AI entities from the spawn managers.
    /// </summary>
    public void SpawnEntities()
    {
        spawnerManagers = FindObjectsOfType<SpawnManager>().Where(x => x.GetComponent<ISpawner>() != null).ToList();
        Type spawnerType = gameSettings.battleSpawnModes.Where(x => x.mode == sessionData.battleMode).First().modeClassType;
        BattleModeSpawner modeSpawner = Activator.CreateInstance(spawnerType) as BattleModeSpawner;
        modeSpawner.Init(this, spawnerManagers[0], spawnerManagers[1]);
        modeSpawner.RunInstance();
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }
}

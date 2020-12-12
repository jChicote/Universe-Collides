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
    //[HideInInspector] public UIHudManager gameplayHUD = null;
    [HideInInspector] public ScoreManager scoreManager;
    public List<SpawnManager> spawnerManagers;

    [Header("UI HUD Managers")]
    public UIStaticHudManager staticHud;
    public UDynamicHUDManager dynamicHud;

    public List<Dictionary<TeamColor, GameObject>> sceneEntities;

    [Header("Travel Detection Spheres")]
    public List<IDetectorSphere> sphereDetectors;

    private GameSettings gameSettings;
    private SessionData sessionData;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;

        gameManager.sceneController = this;
        gameSettings = gameManager.gameSettings;
        sessionData = gameManager.sessionData;

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        sphereDetectors = new List<IDetectorSphere>();
        IDetectorSphere travelSphereInterface;

        foreach (GameObject item in allObjects)
        {
            if (item.GetComponent<IDetectorSphere>() != null)
            {
                travelSphereInterface = item.GetComponent<IDetectorSphere>();
                travelSphereInterface.Init();
                sphereDetectors.Add(travelSphereInterface);
            }
        }

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
        GeneralAudioManager.Instance.onTrackEvent.Invoke(BackgroundOstType.StarWars);

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
        staticHud.Init(dynamicHud);
        dynamicHud.Init(sceneCamera);

        scoreManager = Instantiate(gameSettings.scoreManagerPrefab, transform.position, Quaternion.identity).GetComponent<ScoreManager>();
        scoreManager.Init(dynamicHud.scoreUI);
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

    public void CleanSpheres()
    {
        foreach (IDetectorSphere detector in sphereDetectors)
        {
            detector.CleanSphere();
        }
    }

}

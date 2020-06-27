using System.Collections;
using System.Collections.Generic;
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
        //Debug.Log("Forth");
        gameplayHUD = GameObject.Instantiate(gameSettings.UIHudPrefab,transform.position,Quaternion.identity).GetComponent<UIHudManager>();
        sceneCamera = GameObject.Instantiate(gameSettings.sceneCamera).GetComponent<Camera>();
        playerController = Instantiate(gameSettings.playerPrefab, transform.position, Quaternion.identity).GetComponent<PlayerController>(); 
    }

    public void LoadLevel(int sceneIndex) {
        StartCoroutine(ILoadScene(sceneIndex));
    }

    IEnumerator ILoadScene(int index) {
        //Debug.Log("Second");
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        //Debug.Log("Third");
        OnGameplayStart.Invoke();
    }

    /*IEnumerator IUnloadScene(int index) {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(index);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }*/

}

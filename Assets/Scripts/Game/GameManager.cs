using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Settings")]
    public GameSettings gameSettings;

    [Header("Player Events")]
    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnPlayerDeath;

    public PlayerController playerController;
    public Camera sceneCamera = null;
    public UIHudManager gameplayHUD = null;

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
        Instantiate(gameSettings.audioManagerPrefab, transform.position, Quaternion.identity);
        sceneCamera = GameObject.Instantiate(gameSettings.sceneCamera).GetComponent<Camera>();
        gameplayHUD = GameObject.Instantiate(gameSettings.UIHudPrefab,transform.position,Quaternion.identity).GetComponent<UIHudManager>();
    }

    void Start()
    {
        playerController = Instantiate(gameSettings.playerPrefab, transform.position, Quaternion.identity).GetComponent<PlayerController>(); //change to spawn position
    }

}

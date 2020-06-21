using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Settings")]
    public GameSettings gameSettings;

    [Header("Player Events")]
    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnPlayerDeath;

    public PlayerController playerController;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = Instantiate(gameSettings.playerPrefab, transform.position, Quaternion.identity).GetComponent<PlayerController>(); //change to spawn position
    }
}

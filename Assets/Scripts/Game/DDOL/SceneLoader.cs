using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISceneLoad
{
    void LoadLevel(int sceneIndex);
}

public class SceneLoader : MonoBehaviour, ISceneLoad
{
    private GameManager gameManager;
    private ILoadingScreen loadingScreenUI;

    /// <summary>
    /// Initialises the class.
    /// </summary>
    public void Init()
    {
        gameManager = GameManager.Instance;
        loadingScreenUI = GameManager.Instance.LoadingScreen.GetComponent<ILoadingScreen>();
    }

    /// <summary>
    /// Called to begin loading of following scene.
    /// </summary>
    public void LoadLevel(int sceneIndex)
    {
        //Close main menu
        gameManager.CloseMainMenu();

        StartCoroutine(ILoadScene(sceneIndex));
    }

    IEnumerator ILoadScene(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        loadingScreenUI.DisplayLoadingScreen();

        while (!asyncOperation.isDone)
        {
            loadingScreenUI.UpdateLoadingScreen(Mathf.Clamp01(asyncOperation.progress / 0.9f));
            yield return null;
        }

        gameManager.sceneController = GameObject.FindObjectOfType<SceneController>();
        gameManager.sceneController.OnGameplayStart.Invoke();
        loadingScreenUI.HideLoadingScreen();

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ILoadingScreen
{
    void DisplayLoadingScreen();
    void HideLoadingScreen();
    void UpdateLoadingScreen(float progressValue);
}

public class LoadingScreen : MonoBehaviour, ILoadingScreen
{
    public Slider loadingBar;
    public Image screenImage;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Updates the loading screen UI at runtime when called.
    /// </summary>
    public void UpdateLoadingScreen(float progressValue)
    {
        RevealImageSlides();
        UpdateProgressBar(progressValue);
    }
    
    /// <summary>
    /// Displays the loading screen before progressing to next screen.
    /// </summary>
    public void DisplayLoadingScreen()
    {
        loadingBar.gameObject.SetActive(true);
        screenImage.gameObject.SetActive(true);

        loadingBar.value = 0;
        screenImage.color = Color.black;
    }

    /// <summary>
    /// Animates between different background images.
    /// </summary>
    private void RevealImageSlides()
    {
        if (screenImage == null) return;
        screenImage.color = Color.black;
    }

    /// <summary>
    /// Updates progress value on UI Slider.
    /// </summary>
    private void UpdateProgressBar(float progressValue)
    {
        if (loadingBar == null) return;
        loadingBar.value = progressValue;
    }

    /// <summary>
    /// Hides loading screen after successful loading.
    /// </summary>
    public void HideLoadingScreen()
    {
        Debug.Log("Reached Here");
        loadingBar.value = 0;
        screenImage.color = Color.black;

        loadingBar.gameObject.SetActive(false);
        screenImage.gameObject.SetActive(false);
    }
}

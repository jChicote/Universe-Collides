using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnResume() {
        GameManager.Instance.sessionData.TogglePause();
    }

    public void Respawn() {
        
    }

    public void Settings() {

    }

    public void Restart() {

    }

    public void Quit() {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPowerUpSystem
{
    void SetPowerUpEffect(PowerUpInfo powerUpInfo);
}

public class PowerupSystem : MonoBehaviour, IPowerUpSystem
{
    StatHandler statHandler;

    private PowerUp[] powerUps = new PowerUp[3];

    void Start() {
        statHandler = this.GetComponent<IEntity>().GetStatHandler();
    }

    public void SetPowerUpEffect(PowerUpInfo powerUpInfo) {
        PowerUp powerUp = Activator.CreateInstance(powerUpInfo.classType) as PowerUp;
        powerUp.Init(statHandler, this, powerUpInfo.lifeTime);
        for(int i = 0; i < powerUps.Length; i++) {
            if(powerUps[i] == null){
                powerUps[i] = powerUp;
                //Debug.Log(GameManager.Instance.sceneController.gameplayHUD.powerUpUI);
                GameManager.Instance.sceneController.gameplayHUD.powerUpUI.ChangeImage(powerUpInfo.icon, i);
                return;
            }
        }
    }

    private void OnAbility1(InputValue value) {
        if(powerUps[0] == null) return;
        Debug.Log("Ability 1 active");
        StartCoroutine(RunEffect(powerUps[0], 0));
    }

    private void OnAbility2(InputValue value) {
        if(powerUps[1] == null) return;
        Debug.Log("Ability 2 active");
        StartCoroutine(RunEffect(powerUps[1], 1));
    }

    private void OnAbility3(InputValue value) {
        if(powerUps[2] == null) return;
        Debug.Log("Ability 3 active");
        StartCoroutine(RunEffect(powerUps[2], 2));
    }

    private IEnumerator RunEffect(PowerUp powerUp, int index) {
        powerUp.BeginPowerUp();
        float timer = powerUp.lifeTime;
        
        while(timer > 0) {
            powerUp.RunPowerUp();
            timer -= Time.deltaTime;
            yield return null;
        }

        powerUp.EndPowerUp();
        powerUps[index] = null;
    }
}

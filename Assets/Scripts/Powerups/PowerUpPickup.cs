using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public PowerUpType type;
    private PowerUpInfo powerUpInfo;

    void Start() {
        PowerupSettings powerupSettings = GameManager.Instance.gameSettings.powerupSettings;
        powerUpInfo = powerupSettings.powerUpInfo.Where(x => x.type == type).First();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.GetComponent<IPowerUpSystem>() == null) return;

        IPowerUpSystem system = other.transform.root.GetComponent<IPowerUpSystem>();
        system.SetPowerUpEffect(powerUpInfo);
        Destroy(gameObject);
    } 
}

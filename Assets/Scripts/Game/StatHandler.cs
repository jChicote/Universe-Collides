using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatHandler
{
    [SerializeField] private BaseStats currentStats; 
    [SerializeField] private BaseStats defaultStats;
    BaseEntityController controller;
    bool isHealthRegenerating = false;

    //Invoked by player only
    public HealthEvent OnHealthChanged = new HealthEvent();

    public StatHandler(BaseStats baseStats, EntityType entityType, BaseEntityController controller) {
        this.currentStats = baseStats;
        this.defaultStats = baseStats;
        this.controller = controller;

        //Init variables
        currentFireRate = currentStats.fireRate;
        currentHealth = currentStats.maxHealth;
        damageBuff = currentStats.statModifier.damageModif;

        if(entityType == EntityType.Player) AssignUI();
    }

    public void AssignUI() {
        UIHudManager hudManager = GameManager.Instance.gameplayHUD;
        hudManager.healthBar.Init(currentStats.maxHealth);
        OnHealthChanged.AddListener(hudManager.healthBar.SetHealth);
    }

    [SerializeField] private float currentHealth;
    public float CurrentHealth {
        get {
            return currentHealth;
        }
        set {
            currentHealth = value;
            if(isHealthRegenerating){
                controller.StopCoroutine(RegenerateHealth());
                isHealthRegenerating = false;
            }

            if(currentHealth > currentStats.maxHealth)
                currentHealth = currentStats.maxHealth;

            OnHealthChanged.Invoke(currentHealth);
            controller.StartCoroutine(RegenerateHealth());
        }
    }

    [SerializeField] private float damageBuff = 0;
    public float DamageBuff {
        get {
            return damageBuff;
        }
        set { damageBuff = value; }
    }
    
    [SerializeField] private float currentFireRate;
    public float FireRate {
        get {
            return currentFireRate;
        }
        set {
            currentFireRate = value;
        }
    }

    public float CriticalDamage {
        get {
            DamageInfo damageInfo = currentStats.damageInfo;
            float critDamage = (1 + damageInfo.critChance * (damageInfo.critDMG - 1));
            return critDamage;
        }
    }

    private IEnumerator RegenerateHealth() {
        isHealthRegenerating = true;
        float incrementRate = 0.5f;

        yield return new WaitForSeconds(3);

        //Debug.Log("Begin Regeneration");

        while(currentHealth < currentStats.maxHealth) {
            currentHealth += incrementRate;
            if(currentHealth > currentStats.maxHealth) currentHealth = currentStats.maxHealth;
            OnHealthChanged.Invoke(currentHealth);
            yield return null;
        }

        //Debug.Log("Ended Regeneration");
        isHealthRegenerating = false;
    }

    public void Reset() {
        currentStats = defaultStats;
    }
}

[System.Serializable]
public class HealthEvent : UnityEvent<float> {}

[System.Serializable]
public class BaseStats {
    public float health;
    public float maxHealth;
    public float healthRegen;
    public float healthRegenRate;
    public float fireRate;
    public StatModifier statModifier;
    public DamageInfo damageInfo;
}

[System.Serializable]
public class StatModifier {
    public float fireRateModif;
    public float healthRegenModif;
    public float damageModif;
}

[System.Serializable]
public class DamageInfo {
    public int critDMG;

    [Range(0, 1)]
    public float critChance;
}

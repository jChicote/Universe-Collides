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
    }

    //TODO: Note, damage controller should handle the all of health regeneration,
    //      improper refactoring coupling systems together.
    public float MaxHealth
    {
        get { return currentStats.maxHealth; }
    }

    [SerializeField] private float currentHealth;
    public float CurrentHealth 
    {
        get {
            return currentHealth;
        }
        set {
            currentHealth = value;

            if(currentHealth > currentStats.maxHealth)
                currentHealth = currentStats.maxHealth;
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

    [SerializeField] private float hullResistence;
    public float HullResistence {
        get {
            return hullResistence;
        }
        set {
            hullResistence = value;
        }
    }

    public float CriticalDamage {
        get {
            DamageInfo damageInfo = currentStats.damageInfo;
            float critDamage = (1 + damageInfo.critChance * (damageInfo.critDMG - 1));
            return critDamage;
        }
    }

    public void Reset() {
        currentStats = defaultStats;
    }
}

[System.Serializable]
public class HealthEvent : UnityEvent<float> {}

[System.Serializable]
public class BaseStats {
    public float health; // may need to remove
    public float maxHealth;
    public float healthRegen; //may need to change
    public float healthRegenRate; //may need to remove
    public float hullResistence;
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

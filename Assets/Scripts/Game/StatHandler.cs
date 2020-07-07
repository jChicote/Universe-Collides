using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatHandler
{
    [SerializeField] private BaseStats currentStats; 
    [SerializeField] private BaseStats defaultStats;

    //Invoked by player only
    public UnityEvent OnHealthChanged = new UnityEvent();

    public StatHandler(BaseStats baseStats) {
        this.currentStats = baseStats;
        this.defaultStats = baseStats;

        //Init variables
        currentHealth = currentStats.maxHealth;
        damageBuff = currentStats.statModifier.damageModif;
    }

    [SerializeField] private float currentHealth;
    public float CurrentHealth {
        get {
            return currentHealth;
        }
        set {
            currentHealth = value;

            if(currentStats.health > currentStats.maxHealth)
                currentHealth = currentStats.maxHealth;
        }
    }

    [SerializeField] private float damageBuff = 0;
    public float DamageBuff {
        get {
            return currentStats.statModifier.damageModif;
        }
        set { damageBuff = value; }
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
public class BaseStats {
    public float health;
    public float maxHealth;
    public float healthRegen;
    public float healthRegenRate;
    public float fireRate;
    [HideInInspector] public StatModifier statModifier;
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

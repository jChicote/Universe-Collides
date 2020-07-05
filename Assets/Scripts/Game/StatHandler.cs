using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatHandler
{
    [SerializeField] private BaseStats currentStats; 
    [SerializeField] private BaseStats defaultStats;

    public UnityEvent OnHealthChanged = new UnityEvent();

    public StatHandler(BaseStats baseStats) {
        this.currentStats = baseStats;
        this.defaultStats = baseStats;
    }

    public float CurrentHealth {
        get {
            return currentStats.health;
        }
        set {
            currentStats.health = value;

            if(currentStats.health > currentStats.maxHealth)
                currentStats.health = currentStats.maxHealth;
        }
    }

    public float CriticalDamage {
        get {
            DamageInfo damageInfo = currentStats.damageInfo;
            float critDamage = (1 + damageInfo.critChance * (damageInfo.critDMG - 1));
            return critDamage;
        }
    }

    public float DamageBuff {
        get {
            return currentStats.statModifier.damageModif;
        }
        set { currentStats.statModifier.damageModif = value; }
    }

    public void Reset() {
        currentStats = defaultStats;
    }
}

[System.Serializable]
public class BaseStats {
    public VesselType type;
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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private UIHudManager hudManager;
    private StatHandler actorStats;

    private bool isHealthRegenerating = false;

    // Start is called before the first frame update
    public void Init(StatHandler statHandler)
    {
        this.actorStats = statHandler;

        UIHudManager hudManager = GameManager.Instance.gameplayHUD;
        hudManager.healthBar.Init(actorStats.MaxHealth);
    }

    /// <summary>
    /// Sets the health and updates related UI elements.
    /// </summary>
    public void SetHealthUpdate(float healthValue)
    {
        if (isHealthRegenerating)
        {
            StopCoroutine(RegenerateHealth());
            isHealthRegenerating = false;
        }

        actorStats.CurrentHealth = healthValue;
        hudManager.healthBar.SetHealth(actorStats.CurrentHealth);
        hudManager.healthBar.SetHealth(actorStats.CurrentHealth);
        StartCoroutine(RegenerateHealth());
    }

    private void ActivateActorDeathState()
    {
        if (actorStats.CurrentHealth <= 0)
        {
            BaseState activeState = this.GetComponent<BaseState>();
            activeState.ActivateDeathState();
        }
    }

    private IEnumerator RegenerateHealth()
    {
        isHealthRegenerating = true;
        float incrementRate = 0.5f;

        yield return new WaitForSeconds(3);

        //Debug.Log("Begin Regeneration");

        while (actorStats.CurrentHealth < actorStats.MaxHealth)
        {
            actorStats.CurrentHealth += incrementRate;
            if (actorStats.CurrentHealth > actorStats.MaxHealth) actorStats.CurrentHealth = actorStats.MaxHealth;
            //OnHealthChanged.Invoke(currentHealth);
            hudManager.healthBar.SetHealth(actorStats.CurrentHealth);
            yield return null;
        }

        //Debug.Log("Ended Regeneration");
        isHealthRegenerating = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthSystem : MonoBehaviour, IHealthUpdate
{
    private StatHandler actorStats;

    private bool isHealthRegenerating = false;

    // Start is called before the first frame update
    public void Init(StatHandler statHandler)
    {
        this.actorStats = statHandler;
    }

    /// <summary>
    /// Sets the health and updates related UI elements.
    /// </summary>
    public void SetHealthUpdate(float healthValue)
    {
        if (isHealthRegenerating)
        {
            StopCoroutine("RegenerateHealth");
            isHealthRegenerating = false;
        }

        //Debug.Log("AI Enemy Health: " + actorStats.CurrentHealth);

        actorStats.CurrentHealth = healthValue;
        StopCoroutine("RegenerateHealth");
        StartCoroutine("RegenerateHealth");
    }

    /// <summary>
    /// This sets the death state of the entity
    /// </summary>
    private void ActivateActorDeathState()
    {
        if (actorStats.CurrentHealth <= 0)
        {
            BaseState activeState = this.GetComponent<BaseState>();
            activeState.ActivateDeathState();
        }
    }

    /// <summary>
    /// Called to Regenerate health some time after damage.
    /// </summary>
    private IEnumerator RegenerateHealth()
    {
        isHealthRegenerating = true;
        float incrementRate = 0.5f;

        yield return new WaitForSeconds(3);

        while (actorStats.CurrentHealth < actorStats.MaxHealth)
        {
            actorStats.CurrentHealth += incrementRate;
            if (actorStats.CurrentHealth > actorStats.MaxHealth) actorStats.CurrentHealth = actorStats.MaxHealth;
            yield return null;
        }

        isHealthRegenerating = false;
    }
}

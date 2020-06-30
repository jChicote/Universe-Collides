using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterDamageManager : DamageManager
{
    public override void Init(BaseState state, BaseWeaponSystem weaponSystem) {
        this.shipState = state;
        this.weaponSystem = weaponSystem;
    }

    public override void OnDeath()
    {
        Debug.Log("is Dead");
    }

    public override void OnRecievedDamage(DamageInfo damageInfo, string colliderName)
    {
        float averageDamage = damageInfo.normalDMG * (1 + 0.1f * (damageInfo.critDMG - 1));
        //Debug.Log("is Damaging");
        DamageComponent(colliderName);
    }

    private void DamageComponent(string colliderName) {

        ShipComponent component = null;

        for(int i = 0; i < components.Length; i++){
            if(components[i].type.ToString().Equals(colliderName)) {
                component = components[i];
            }   
        }

        if(component == null) return;
        component.hits++;
        if(component.hits > component.maxHits) DisableComponent(component);

        //Debug.Log(component.hits);
        //Debug.Log(component.isDamaged);
        //Debug.Log("Is engine damaged? : " + currentState.isEngineDamaged);
    }

    private void DisableComponent(ShipComponent component) {
        component.isDamaged = true;
        shipState.isEngineDamaged = true;
        StartCoroutine(RepairComponent(component, 3f));
    }

    private IEnumerator RepairComponent(ShipComponent component, float tickLength) {
        float timeUntilRepaired = tickLength;

        while(timeUntilRepaired > 0) {
            timeUntilRepaired -= Time.deltaTime;
            yield return null;
        }

        component.hits = 0;
        component.isDamaged = false;
        shipState.isEngineDamaged = false;
    }
}

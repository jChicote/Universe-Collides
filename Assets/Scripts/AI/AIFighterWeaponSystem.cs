using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSystems;

public interface ISetWeaponTarget
{
    void SetTarget();
}

public class AIFighterWeaponSystem : FighterWeaponSystem, ISetWeaponTarget
{
    private ITargetFinder targetFinder;

    public override void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats, SceneController sceneController)
    {
        base.Init(objectID, controller, weaponsActive, shipStats, sceneController);

        //  Retrieves interface for the target finder
        targetFinder = this.GetComponent<ITargetFinder>();

        //  creates new target checker for relative direction check
        targetChecker = new TargetDirectionCheck();
    }

    public override void RunSystem()
    {
        base.RunSystem();
    }

    /// <summary>
    /// Sets the weapon system target
    /// </summary>
    public void SetTarget()
    {
        this.target = targetFinder.GetTarget();
        Debug.Log(target);
    }
}

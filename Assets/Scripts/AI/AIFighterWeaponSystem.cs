using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSystems;

public interface IAssignTarget
{
    void AssignTarget(GameObject target);
}

public class AIFighterWeaponSystem : FighterWeaponSystem, IAssignTarget
{
    public override void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats, SceneController sceneController)
    {
        base.Init(objectID, controller, weaponsActive, shipStats, sceneController);

        targetChecker = new TargetDirectionCheck();
    }

    public override void RunSystem()
    {
        base.RunSystem();
    }

    public void AssignTarget(GameObject target)
    {
        this.target = target;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITargetFinder
{
    void FindTarget();
    GameObject GetTarget();
}


public class TargetFinder : MonoBehaviour, ITargetFinder
{
    private GameObject currentTarget;

    //  interface for the team for the attached entity
    private ITeam currentTeam;

    /// <summary>
    /// Finds the closest valid enemy target to the current entity
    /// </summary>
    public void FindTarget()
    {
        this.currentTarget = FindClosestEnemy();
    }

    /// <summary>
    /// Returns the value fo the current target (either a existing target or null)
    /// </summary>
    public GameObject GetTarget()
    {
        return currentTarget;
    }

    /// <summary>
    /// Loads team assigned to this entity
    /// </summary>
    private void LoadCurrentTeam()
    {
        currentTeam = this.GetComponent<ITeam>();
    }

    /// <summary>
    /// Finds the closest enemy contained within the sphere area
    /// </summary>
    public GameObject FindClosestEnemy()
    {
        //Ensure thhat the current team is set
        if(currentTeam == null)
        {
            LoadCurrentTeam();
        }

        IDetectorSphere sphereDetector = FindClosestSphereDetector();

        if (sphereDetector.GetDetectedEntities().Count == 0) return null;

        ITeam entityTeam = null;
        GameObject closestEnemy = null;
        float closestDistance = 100000;

        foreach (GameObject entity in sphereDetector.GetDetectedEntities())
        {
            // Pass whether object has NOT beed removed or missing
            if (entity != null)
            {
                float distance = Vector3.Distance(transform.position, entity.transform.position);
                entityTeam = entity.GetComponent<ITeam>();

                if (distance < closestDistance && currentTeam.GetTeamColor() != entityTeam.GetTeamColor())
                {
                    closestDistance = distance;
                    closestEnemy = entity;
                }
            }
        }

        return closestEnemy;
    }

    /// <summary>
    /// Finds the closest travel sphere relative to the entity
    /// </summary>
    private IDetectorSphere FindClosestSphereDetector()
    {
        SceneController sceneController = GameManager.Instance.sceneController;
        IDetectorSphere closestSphere = null;
        float closestDistance = 100000;

        foreach(IDetectorSphere sphere in sceneController.sphereDetectors) 
        {
            float distance = Vector3.Distance(transform.position, sphere.GetSpherePosition());

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSphere = sphere;
            }
        }

        return closestSphere;
    }
}

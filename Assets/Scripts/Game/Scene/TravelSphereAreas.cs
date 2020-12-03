using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectorSphere
{
    void Init();
    Vector3 GetSpherePosition();
    List<GameObject> GetDetectedEntities();
    void CleanSphere();
}

public class TravelSphereAreas : MonoBehaviour, IDetectorSphere
{
    private List<GameObject> detectedEntities;

    public void Init()
    {
        detectedEntities = new List<GameObject>();
    }

    /// <summary>
    /// Returns the sphere position of this detector
    /// </summary>
    public Vector3 GetSpherePosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Returns reference to detected entitites
    /// </summary>
    public List<GameObject> GetDetectedEntities()
    {
        CleanSphere();
        return detectedEntities;
    }

    /// <summary>
    /// Cleans array from any missing object 
    /// </summary>
    public void CleanSphere()
    {
        List<GameObject> cleanedArray = new List<GameObject>();

        foreach (GameObject entity in detectedEntities)
        {
            if (entity != null)
            {
                cleanedArray.Add(entity);
            }
        }

        //   Replaces old array with cleaned array
        detectedEntities = cleanedArray;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IEntity>() != null)
        {
            detectedEntities.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IEntity>() == null) return;

        if (detectedEntities.Contains(other.gameObject))
        {
            detectedEntities.Remove(other.gameObject);
        }

        CleanSphere();
    }
}

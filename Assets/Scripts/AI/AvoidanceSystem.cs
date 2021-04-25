using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceSystem : MonoBehaviour
{
    //RaycastHit hit;
    Vector3 raycastOffset = Vector3.zero;

    Vector3 left;
    Vector3 right;
    Vector3 up;
    Vector3 down;

    Vector3 leftDir;
    Vector3 rightDir;
    Vector3 upDir;
    Vector3 downDir;

    bool whiskerEnabled;

    public bool DetectCollision()
    {
        if(!whiskerEnabled) return false;

        raycastOffset = Vector3.zero;
        left = transform.localPosition - (transform.right * 3f);
        right = transform.localPosition + (transform.right * 3f);
        up = transform.localPosition + (transform.up * 3f);
        down = transform.localPosition - (transform.up * 3f);

        leftDir = transform.forward + -transform.right*0.4f;
        rightDir = transform.forward + transform.right*0.4f;
        upDir = transform.forward + transform.up*0.2f;
        downDir = transform.forward + -transform.up*0.4f;

        //Debug.DrawRay(left, leftDir *  25f, Color.cyan);
        //Debug.DrawRay(right, rightDir *  25f, Color.cyan);
        //Debug.DrawRay(up, upDir *  15f, Color.yellow);
        //Debug.DrawRay(down, downDir *  15f, Color.yellow);

        EngageWhiskers();

        if(raycastOffset != Vector3.zero) {
            Quaternion avoidanceRot = Quaternion.LookRotation(raycastOffset * 40);
            transform.rotation = Quaternion.Slerp(transform.rotation, avoidanceRot, Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    private void EngageWhiskers() {
        if (Physics.Raycast(left, leftDir, 25f))
        {
            raycastOffset += transform.right;
        }
        else if(Physics.Raycast(right, rightDir, 25f))
        {
            raycastOffset -= Vector3.right;
        }
        
        if (Physics.Raycast(up, upDir, 15f))
        {
            raycastOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, downDir, 15f))
        {
            raycastOffset += Vector3.up;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        whiskerEnabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        whiskerEnabled = false;
    }
}
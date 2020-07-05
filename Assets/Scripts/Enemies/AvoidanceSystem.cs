using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceSystem : MonoBehaviour
{
    RaycastHit hit;
    Vector3 raycastOffset = Vector3.zero;

    Vector3 left;
    Vector3 right;
    Vector3 forward;
    Vector3 up;
    Vector3 down;

    void Start()
    {
        
    }

    public bool DetectCollision()
    {
        raycastOffset = Vector3.zero;
        left = transform.localPosition - (transform.right * 3f);// + (transform.forward * 3));
        right = transform.localPosition + (transform.right * 3f);// + (transform.forward * 3));
        //forward = transform.position + transform.forward * 3f;
        //up = transform.position + (transform.up * 3f + (transform.forward * -3));
        //down = transform.position - ((transform.up * 3f) + (transform.forward * 3));

        Debug.DrawRay(left, transform.forward *  30f, Color.cyan);
        Debug.DrawRay(right, transform.forward *  30f, Color.cyan);
        //Debug.DrawRay(forward, transform.forward * 40f, Color.cyan);
        Debug.DrawRay(up, transform.forward *  20f, Color.cyan);
        Debug.DrawRay(down, transform.forward *  20f, Color.cyan);

        /*if (Physics.Raycast(forward, transform.forward, 40f)) {
            raycastOffset += hit.normal * 50f;
            Debug.DrawRay(hit.point, hit.normal * 20f, Color.red);
            //CalculateRotation();
            //return true;
        }
        else */
        if (Physics.Raycast(left, transform.forward, out hit,  30f))
        {
            raycastOffset = transform.right;
            Debug.DrawRay(hit.point, hit.normal * 20f, Color.red);
            //CalculateRotation();
            //return true;
        }
        else if(Physics.Raycast(right, transform.forward, out hit,  30f))
        {
            //raycastOffset -= Vector3.up;
            raycastOffset = -transform.right;
            Debug.DrawRay(hit.point, hit.normal * 20f, Color.red);
            //CalculateRotation();
            //return true;
        }
        /*else if (Physics.Raycast(up, transform.forward, out hit,  20f))
        {
            raycastOffset -= Vector3.right;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, 20f))
        {
            raycastOffset += Vector3.right;
        }*/

        if(raycastOffset != Vector3.zero) {
            //transform.Rotate(raycastOffset * 150f * Time.fixedDeltaTime);
            //Vector3 avoidanceDir = raycastOffset;
            Quaternion avoidanceRot = Quaternion.LookRotation(raycastOffset * 20);
            transform.rotation = Quaternion.Slerp(transform.rotation, avoidanceRot, Time.fixedDeltaTime);
            return true;
        }
        
        return false;
    }

    private void CalculateRotation() {
        if(raycastOffset != Vector3.zero) {
            //transform.Rotate(raycastOffset * 150f * Time.fixedDeltaTime);
            //Vector3 avoidanceDir = raycastOffset;
            Quaternion avoidanceRot = Quaternion.LookRotation(raycastOffset * 20);
            transform.rotation = Quaternion.Slerp(transform.rotation, avoidanceRot, Time.fixedDeltaTime);
        }
        //Vector3 avoidanceDir = raycastOffset;
        //Quaternion avoidanceRot = Quaternion.LookRotation(avoidanceDir);
        //transform.rotation = Quaternion.Slerp(transform.rotation, avoidanceRot, Time.fixedDeltaTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public interface IExplosionShake
{
    void TriggerExplosionShake(Vector3 explosionPosition);
}

public class CameraExternalShake : MonoBehaviour, IExplosionShake
{
    private bool isShaking = false;
    private CameraShaker cameraShaker;
    private float decrementVal = 0;

    public void Init(CinemachineVirtualCamera virtualCamera)
    {
        cameraShaker = new CameraShaker();
        cameraShaker.Init(virtualCamera);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (!isShaking) return;

       if(cameraShaker.DecreaseShake(decrementVal))
       {
            isShaking = false;
       }
    }

    public void TriggerExplosionShake(Vector3 explosionPosition)
    {
        //Set Camera Clamp
        cameraShaker.SetClamp(0, 2);
        isShaking = true;

        float startingShakeVal = Vector3.Magnitude(explosionPosition - transform.position);
        startingShakeVal = 2 * (startingShakeVal / 100f);
        cameraShaker.SetShakeValue(2);
        decrementVal = 0.01f;
        
    }
}

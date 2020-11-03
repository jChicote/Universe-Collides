using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPausable {
    void Pause();
    void UnPause();
}

namespace PlayerSystems
{
    public abstract class PlayerState : BaseState
    {
        [HideInInspector] public PlayerSettings playerSettings;
        [HideInInspector] public PlayerController controller;
        [HideInInspector] public Rigidbody playerRB;

        public void PlayerDeath()
        {
            controller.SetState<DeathState>();
        }

        public override void OnRecievedDamage(float damage, string id, SoundType soundType)
        {
            base.OnRecievedDamage(damage, id, soundType);
            controller.audioSystem.PlaySoundEffect(soundType);
        }
    }
}
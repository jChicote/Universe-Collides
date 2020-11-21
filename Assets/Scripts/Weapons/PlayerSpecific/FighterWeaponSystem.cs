using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICannonTransform
{
    Transform GetTransform();
}

namespace PlayerSystems
{
    public class FighterWeaponSystem : BaseWeaponSystem
    {
        [Header("Weapon Switches")]
        public bool canShootPrimary = false;
        public bool canShootAlternative = false;
        public bool canShootSecondary = false;

        [Header("Weapon Loadouts")]
        public GameObject[] primaryWeapons;
        public GameObject[] alternativeWeapons;
        public GameObject[] secondaryWeapons;

        //Weapon Firing handlers
        private ShootingHandler primaryWeaponHandler;
        private ShootingHandler alternativeWeaponHandler;
        private ShootingHandler secondaryWeaponHandler;

        private bool isPaused = false;

        public override void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats, SceneController sceneController)
        {
            base.Init(objectID, controller, weaponsActive, shipStats, sceneController);

            //Create the aim assist object
            aimAssist = new AimAssist();
            aimAssist.Init(this, sceneController.dynamicHud.aimSightUI);

            SetupWeapons();
        }

        /// <summary>
        /// Sets up the weapon collections for Primary, Alternative and Secondary Weapons
        /// </summary>
        private void SetupWeapons()
        {
            List<IWeaponItem> primary = new List<IWeaponItem>();
            List<IWeaponItem> alternative = new List<IWeaponItem>();
            List<IWeaponItem> secondary = new List<IWeaponItem>();

            IWeaponItem primaryInstance;
            IWeaponItem alternativeInstance;
            IWeaponItem secondaryInstance;

            for (int i = 0; i < primaryWeapons.Length; i++)
            {
                primaryInstance = primaryWeapons[i].GetComponent<IWeaponItem>();
                ProjectileType type = shipStats.primaryWeaponInfo.weaponData.projectileType;
                primaryInstance.Init(objectID, controller.statHandler.FireRate, controller.audioSystem, aimAssist, type);
                primary.Add(primaryInstance);
            }

            for (int i = 0; i < alternativeWeapons.Length; i++)
            {
                alternativeInstance = alternativeWeapons[i].GetComponent<IWeaponItem>();
                ProjectileType type = shipStats.primaryWeaponInfo.weaponData.projectileType;
                alternativeInstance.Init(objectID, controller.statHandler.FireRate, controller.audioSystem, aimAssist, type);
                alternative.Add(alternativeInstance);
            }

            for (int i = 0; i < secondaryWeapons.Length; i++)
            {
                secondaryInstance = secondaryWeapons[i].GetComponent<IWeaponItem>();
                ProjectileType type = shipStats.primaryWeaponInfo.weaponData.projectileType;
                secondaryInstance.Init(objectID, controller.statHandler.FireRate, controller.audioSystem, aimAssist, type);
                secondary.Add(secondaryInstance);
            }

            SetupWeaponHandler(primary, alternative, secondary);
        }

        private void SetupWeaponHandler(List<IWeaponItem> primaryWeapons, List<IWeaponItem> alternativeWeapons, List<IWeaponItem> secondaryWeapons)
        {
            if(shipStats.primaryWeaponInfo.isEnabled)
            {
                primaryWeaponHandler = this.gameObject.AddComponent<ShootingHandler>();
                primaryWeaponHandler.Init(controller.statHandler, primaryWeapons, shipStats, shipStats.primaryWeaponInfo);
            }
            
            if (shipStats.alternativeWeaponInfo.isEnabled)
            {
                alternativeWeaponHandler = this.gameObject.AddComponent<ShootingHandler>();
                alternativeWeaponHandler.Init(controller.statHandler, alternativeWeapons, shipStats, shipStats.alternativeWeaponInfo);
            }

            if (shipStats.secondaryWeaponInfo.isEnabled)
            {
                secondaryWeaponHandler = this.gameObject.AddComponent<ShootingHandler>();
                secondaryWeaponHandler.Init(controller.statHandler, secondaryWeapons, shipStats, shipStats.secondaryWeaponInfo);
            }
        }

        /// <summary>
        /// Runs the various wepapon system handlers on the weapon
        /// </summary>
        public override void RunSystem()
        {
            if (isPaused)
            {
                Debug.Log("Is Paused");
                return;
            }
            aimAssist.SetTargetInAimRange();

            if (canShootPrimary && primaryWeaponHandler != null)
            {
                primaryWeaponHandler.RunWeaponsFire();
            }

            if (canShootAlternative && alternativeWeaponHandler != null)
            {
                alternativeWeaponHandler.RunWeaponsFire();
            }

            if (canShootSecondary && secondaryWeaponHandler != null)
            {
                secondaryWeaponHandler.RunWeaponsFire();
            }
        }

        public override void SetAimPosition(float speed)
        {
            float timeAhead = Mathf.Clamp(speed * 7, 3, 200);
            gameManager.sceneController.dynamicHud.aimSightUI.SetAimPosition(transform.position, transform.forward, timeAhead, controller.cameraController.isFocused);
        }

        public override void SetFireRate(float fireRate, bool isParallel) //TODO: Change for dynamic modifications of either system
        {
            //cannon.ModifyStats(controller.statHandler.FireRate);
        }

        public override void SetPrimaryFire(InputValue value)
        {
            canShootPrimary = value.isPressed;
        }

        public void SetSecondPrimaryFire(InputValue value)
        {

        }

        public override void SetSecondaryFire(InputValue value)
        {
            canShootSecondary = value.isPressed;
        }
    }
}



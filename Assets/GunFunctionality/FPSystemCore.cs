using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.UI;
namespace FPSystem.Core
{
    public class FPSystemCore : MonoBehaviour
    {
        [SerializeField] private bool debugMode;            // Set to true to enable debug outputs, set to false or ignore to disable


        // ***CAREFUL CHANGING THESE VALUES*** \\
        [SerializeField] protected int ammoMax;               // Used to set the max ammo for a weapon's magazine, set to -1 for infinite ammo
        [SerializeField] private int ammoCurrent;           // Used to track/set the amount of ammo currently in a weapon's magazine
        [SerializeField] private int ammoConsumption;       // Used to set the amount of ammo used per shot (EXAMPLE USE: Double-Barrel shotgun firing both barrels at once)

        [SerializeField] private int reserveAmmoMax;        // Used to set the maximum amount of ammmo a weapon is allowed to have in reserve
        [SerializeField] private int reserveAmmoCurrent;    // used to track/set the amount of ammo currently waiting in reserve

        [SerializeField] private float attackSpeed;         // used to set the fire-rate/attack speed of a weapon

        [SerializeField] private float attackTime;          // used if you want to set a delay between "attacking" and "shooting"

        [SerializeField] private float reloadSpeed;         // used to set a reload speed for a given weapon. Leave as 0 for instant reload

        [SerializeField] private Transform bulletOrigin;    // used to set the point at which physical bullets and raycasts come from.
        [SerializeField] private float weaponRange;         // Used to set the maximum range that a bullet/raycast will travel once fired
        [SerializeField] private TargetDummyScript targetHit;

        [SerializeField] private float weaponDamage;

        protected void Attack(bool isRaycastWeapon)
        {
            switch (ammoCurrent)
            {
                case > 0:
                    Shoot(true, isRaycastWeapon);
                 break;

                case 0:
                    Reload();
                    if (debugMode == true) 
                    {
                        Debug.Log("Player tried to use a weapon without ammo");
                    }
                 break;

                case -1:
                    Shoot(false, isRaycastWeapon);
                    break;
            }
        }

        private void Shoot(bool usesAmmo, bool raycastFiring)
        {
            if (raycastFiring == true)
            {
                SendRaycast();
                if (targetHit != null)
                {
                    HitATarget(weaponDamage);

                }
            }





            if (usesAmmo == true)
            {
                ammoCurrent = ammoCurrent - ammoConsumption;
                if (ammoCurrent < 0)
                {
                    ammoCurrent = 0;
                }
                if (debugMode == true)
                {
                    Debug.Log("Shot fired using: " + ammoConsumption + "ammo. ammo left: " + ammoCurrent);
                }
            }
            else 
            { 
                // EMPTY
            }
        }

        protected void Reload()
        {
            int ammoNeededToReload;
            ammoNeededToReload = ammoMax - ammoCurrent;
            if (ammoNeededToReload > reserveAmmoCurrent)
            {
                ammoCurrent = reserveAmmoCurrent;
                reserveAmmoCurrent = 0;
            }
            else
            {
                reserveAmmoCurrent = reserveAmmoCurrent - ammoNeededToReload;
                ammoCurrent = ammoMax;
            }

            if (debugMode == true)
            {
                Debug.Log("Reloaded. Using: " + ammoNeededToReload + " rounds. Ammo currently at: " + ammoCurrent + " / " + ammoMax + ". Reserve Ammo remaining" + reserveAmmoCurrent + " / " + reserveAmmoMax);
            }

        }

        private void SendRaycast()
        {
            RaycastHit hitObject;


            if (Physics.Raycast(bulletOrigin.position, bulletOrigin.TransformDirection(Vector3.forward), out hitObject, weaponRange))
            {
                Debug.DrawRay(bulletOrigin.position, transform.TransformDirection(Vector3.forward) * hitObject.distance, Color.yellow);
                Debug.Log("Hit " + hitObject.transform.name);

                if (hitObject.transform != null)
                {
                    targetHit = hitObject.transform.GetComponent<TargetDummyScript>();
                }
                else
                {
                    targetHit = null;
                }
            }  
        }

        private void HitATarget(float damageDealt)
        {

            targetHit.BeenHit(damageDealt);
            Debug.Log("Enemy is at " + targetHit.GetComponent<TargetDummyScript>().objectHealth + " HP");
        }

    }


    }
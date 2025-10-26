using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.UI;
namespace FPSystem.Core
{
    public class FPSystemCore : MonoBehaviour
    {
        [SerializeField] private bool debugMode;            // Set to true to enable debug outputs, set to false or ignore to disable


        // ***CAREFUL CHANGING THESE VALUES*** \\
        [SerializeField] private int ammoMax;               // Used to set the max ammo for a weapon's magazine, set to -1 for infinite ammo
        [SerializeField] private int ammoCurrent;           // Used to track/set the amount of ammo currently in a weapon's magazine
        [SerializeField] private int ammoConsumption;       // Used to set the amount of ammo used per shot (EXAMPLE USE: Double-Barrel shotgun firing both barrels at once)

        [SerializeField] private int reserveAmmoMax;        // Used to set the maximum amount of ammmo a weapon is allowed to have in reserve
        [SerializeField] private int reserveAmmoCurrent;    // used to track/set the amount of ammo currently waiting in reserve

        [SerializeField] private float attackSpeed;         // used to set the fire-rate/attack speed of a weapon

        [SerializeField] private float attackTime;          // used if you want to set a delay between "attacking" and "shooting"

        [SerializeField] private float reloadSpeed;         // used to set a reload speed for a given weapon. Leave as 0 for instant reload


        protected void Attack()
        {
            switch (ammoCurrent)
            {
                case > 0:
                    Shoot(true);
                 break;

                case 0:
                    Reload();
                    if (debugMode == true) 
                    {
                        Debug.Log("Player tried to use a weapon without ammo");
                    }
                 break;

                case -1:
                    Shoot(false);
                    break;
            }
        }

        private void Shoot(bool usesAmmo)
        {
            // REMINDER**Insert raycast/projectile firing here**REMINDER \\
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

    }


    }
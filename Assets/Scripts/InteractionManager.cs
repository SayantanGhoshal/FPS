using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoverdWeapon = null;
    public AmmoBox hoverdAmmoBox = null;
    public Throwable hoverdThrowable = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                // Disable the outlien of previously selected item
                if (hoverdWeapon)
                {
                    hoverdWeapon.GetComponent<Outline>().enabled = false;
                }

                hoverdWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoverdWeapon.GetComponent<Outline>().enabled = true;
                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoverdWeapon)
                {
                    hoverdWeapon.GetComponent<Outline>().enabled = false;
                }
            }

            // AmmoBox
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                // Disable the outlien of previously selected item
                if (hoverdAmmoBox)
                {
                    hoverdAmmoBox.GetComponent<Outline>().enabled = false;
                }

                hoverdAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoverdAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupAmmo(hoverdAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoverdAmmoBox)
                {
                    hoverdAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }

            // Throwable
            if (objectHitByRaycast.GetComponent<Throwable>())
            {
                // Disable the outlien of previously selected item
                if (hoverdThrowable)
                {
                    hoverdThrowable.GetComponent<Outline>().enabled = false;
                }

                hoverdThrowable = objectHitByRaycast.gameObject.GetComponent<Throwable>();
                hoverdThrowable.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupThrowable(hoverdThrowable);
                }
            }
            else
            {
                if (hoverdThrowable)
                {
                    hoverdThrowable.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}

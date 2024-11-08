using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder);
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            // Initialize all related methods with false
            TurnVisual(false);
            weapon.transform.SetParent(transform, false);
            weapon.transform.localPosition = Vector3.zero;
            weapon.parentTransform = transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon.transform.parent == transform && other.CompareTag("Player"))
        {
            // Get the current weapon and its pickup point
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>();
            if (currentWeapon != null)
            {
                currentWeapon.transform.SetParent(currentWeapon.parentTransform);
                currentWeapon.transform.localPosition = Vector3.zero;
                TurnVisual(false, currentWeapon);
            }

            // Assign the new weapon to the player
            weapon.transform.SetParent(other.transform);
            // Adjust the local position of the weapon relative to the player
            weapon.transform.localPosition = new Vector3(0, 0, 0); // Adjust this value as needed
            TurnVisual(true);
        }
    }

    void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            // Enable or disable all MonoBehaviour components in the Weapon object
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = on;
            }

            // Enable or disable the Animator component
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = on;
            }

            // Enable or disable the renderer components
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = on;
            }
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            // Enable or disable all MonoBehaviour components in the Weapon object
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = on;
            }

            // Enable or disable the Animator component
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = on;
            }

            // Enable or disable the renderer components
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = on;
            }
        }
    }
}
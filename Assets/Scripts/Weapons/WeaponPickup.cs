using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Tempat penyimpanan senjata
    private Weapon weapon; // Senjata yang akan diambil

    void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder); // Membuat instance baru dari senjata yang disimpan
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            // Inisialisasi semua metode terkait dengan false
            TurnVisual(false);
            weapon.transform.SetParent(transform, false); // Menetapkan weapon sebagai child dari objek ini
            weapon.transform.localPosition = Vector3.zero; // Mengatur posisi lokal senjata ke (0,0,0)
            weapon.parentTransform = transform; // Menyimpan transformasi parent senjata
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon.transform.parent == transform && other.CompareTag("Player"))
        {
            // Mendapatkan senjata saat ini dan titik pengambilannya
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>();
            if (currentWeapon != null)
            {
                currentWeapon.transform.SetParent(currentWeapon.parentTransform); // Mengembalikan senjata saat ini ke induknya
                currentWeapon.transform.localPosition = Vector3.zero; // Mengatur posisi lokal senjata saat ini ke (0,0,0)
                TurnVisual(false, currentWeapon); // Menonaktifkan visual senjata saat ini
            }

            // Menetapkan senjata baru ke pemain
            weapon.transform.SetParent(other.transform);
            // Menyesuaikan posisi lokal senjata relatif terhadap pemain
            weapon.transform.localPosition = new Vector3(0, 0, 0); // Sesuaikan nilai ini sesuai kebutuhan
            TurnVisual(true); // Mengaktifkan visual senjata baru
        }
    }

    void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            // Mengaktifkan atau menonaktifkan semua komponen MonoBehaviour di objek Weapon
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = on;
            }

            // Mengaktifkan atau menonaktifkan komponen Animator
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = on;
            }

            // Mengaktifkan atau menonaktifkan komponen renderer
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
            // Mengaktifkan atau menonaktifkan semua komponen MonoBehaviour di objek Weapon
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = on;
            }

            // Mengaktifkan atau menonaktifkan komponen Animator
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = on;
            }

            // Mengaktifkan atau menonaktifkan komponen renderer
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = on;
            }
        }
    }
}
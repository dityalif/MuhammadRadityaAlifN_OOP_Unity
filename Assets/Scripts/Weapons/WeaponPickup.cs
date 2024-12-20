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
            weapon.transform.localScale = new Vector3(1.2f, 1.2f, 0); // Mengatur skala lokal senjata ke (1,1,1)
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
                currentWeapon.isPickedUp = false; // Menonaktifkan senjata saat ini
                currentWeapon.transform.SetParent(currentWeapon.parentTransform); // Mengembalikan senjata saat ini ke induknya
                currentWeapon.transform.localPosition = Vector3.zero; // Mengatur posisi lokal senjata saat ini ke (0,0,0)
                currentWeapon.transform.localScale = new Vector3(1.2f, 1.2f, 0); // Mengatur skala lokal senjata saat ini ke (1,1,1)
                TurnVisual(false, currentWeapon); // Menonaktifkan visual senjata saat ini
            }

            // Menetapkan senjata baru ke pemain
            weapon.transform.SetParent(other.transform);
            // Menyesuaikan posisi lokal senjata relatif terhadap pemain
            weapon.transform.localPosition = Vector3.zero; // Mengatur posisi lokal senjata ke (0,0,0)
            weapon.transform.localScale = new Vector3(1.2f, 1.2f, 0); // Mengatur skala lokal senjata ke (1,1,1)
            TurnVisual(true); // Mengaktifkan visual senjata baru

            // Atur isPickedUp menjadi true
            weapon.isPickedUp = true;
        }
    }

    void TurnVisual(bool on, Weapon targetWeapon = null)
    {
        Weapon weaponToModify = targetWeapon ?? weapon;
        if (weaponToModify == null) return;

        // Hanya mengatur komponen visual
        foreach (var renderer in weaponToModify.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }

        // Animator jika ada
        if (weaponToModify.GetComponentInChildren<Animator>() is Animator animator)
        {
            animator.enabled = on;
        }
    }
}
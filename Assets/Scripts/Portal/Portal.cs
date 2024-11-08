using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed; // Kecepatan pergerakan portal
    [SerializeField] float rotateSpeed; // Kecepatan rotasi portal
    Vector2 newPosition; // Posisi baru portal
    Animator animator; // Animator component

    void Start()
    {
        ChangePosition();
        animator = GetComponent<Animator>(); // Mengambil komponen Animator dari game object
    }
    
    void Update()
    {
        // Memindahkan portal menuju posisi baru
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Memutar portal
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Cek apakah player memiliki weapon
        if (GameObject.Find("Player").GetComponentInChildren<Weapon>() != null)
        {
            // Player memiliki senjata, mengaktifkan portal
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            // Player tidak memiliki senjata, menonaktifkan portal
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Menjalankan animasi Portal
            foreach (Transform child in GameManager.Instance.transform)
            {
                if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
                {
                    child.gameObject.SetActive(true);
                }
            }
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        // Mengubah posisi baru secara acak dalam batas tertentu
        newPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
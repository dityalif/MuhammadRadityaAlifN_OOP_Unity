using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        // Memastikan hanya ada satu instance GameManager yang aktif
        if (Instance != null && Instance != this)
        {
            Destroy(this); // Destroy instance baru jika sudah ada instance lain
            return;
        }

        Instance = this; // Set instance saat ini sebagai instance aktif

        // Mengambil komponen LevelManager dari child GameObject
        LevelManager = GetComponentInChildren<LevelManager>();

        // Mencegah GameManager untuk didestroy saat memuat scene baru
        DontDestroyOnLoad(gameObject);

        // Mencegah camera didestroy saat memuat scene baru
        GameObject camera = GameObject.Find("Camera");
        if (camera != null)
        {
            DontDestroyOnLoad(camera);
        }

        // Mencegah player didestroy saat memuat scene baru
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            DontDestroyOnLoad(player);
        }

        // Menonaktifkan GameObject UI saat start game
        Transform uiTransform = transform.Find("UI");
        if (uiTransform != null)
        {
            uiTransform.gameObject.SetActive(false);
        }
    }
}

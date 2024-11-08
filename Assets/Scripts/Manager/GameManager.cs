using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Camera"));
        DontDestroyOnLoad(GameObject.FindWithTag("Player"));

        RemoveAllObjectsExceptCameraAndPlayer();
    }

    private void RemoveAllObjectsExceptCameraAndPlayer()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("MainCamera") || obj.CompareTag("Player") || obj == gameObject)
            {
                continue;
            }
            Destroy(obj);
        }
    }
}

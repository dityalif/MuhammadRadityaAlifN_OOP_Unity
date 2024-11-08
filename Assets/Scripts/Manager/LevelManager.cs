using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        // Pola Singleton untuk memastikan hanya ada satu instance dari LevelManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan destroy instance ini saat scene baru
        }
        else
        {
            Destroy(gameObject); // Hancurkan game object jika instance sudah ada
        }
    }

    // Coroutine untuk memuat scene secara asynchronous
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1); // Tunggu selama 1 detik

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName); // Mulai memuat scene secara asynchronous
        while (!asyncLoad.isDone)
        {
            yield return null; // Tunggu sampai scene selesai dimuat
        }

        // Pastikan instance Player tidak null sebelum mengatur posisi
        if (Player.Instance != null)
        {
            Player.Instance.transform.position = new Vector3(0, -4.5f, 0); // Atur posisi Player
        }

        // Nonaktifkan GameObject UI setelah scene dimuat
        Transform uiTransform = GameManager.Instance.transform.Find("UI");
        if (uiTransform != null)
        {
            uiTransform.gameObject.SetActive(false); // Nonaktifkan UI
        }
    }

    // Method untuk memulai coroutine LoadSceneAsync
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName)); // Mulai coroutine untuk memuat scene
    }
}

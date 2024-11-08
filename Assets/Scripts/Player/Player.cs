using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    public static Player Instance { get; private set; }

    void Awake()
    {
        // Singleton pattern untuk memastikan hanya ada satu instance Player
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan instance ini saat memuat scene baru
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Mendapatkan komponen PlayerMovement dari GameObject
        playerMovement = GetComponent<PlayerMovement>();
        // Mendapatkan komponen Animator dari GameObject 
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        // Mengatur parameter "IsMoving" pada animator berdasarkan status pergerakan player
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
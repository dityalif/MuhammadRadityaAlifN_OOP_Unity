using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    public static Player instance { get; private set; }
    
    void Start()
    {
        // Mendapatkan komponen PlayerMovement dari GameObject
        playerMovement = GetComponent<PlayerMovement>();
        // Mendapatkan komponen Animator dari GameObject 
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    void Awake()
    {
        // Singleton pattern untuk memastikan hanya ada satu instance Player
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
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
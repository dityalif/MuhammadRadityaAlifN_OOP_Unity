using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    private Vector2 moveDirection; 
    private Vector2 moveVelocity;  
    private Vector2 moveFriction;  
    private Vector2 stopFriction;  
    private Rigidbody2D rb;        

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Menghitung nilai awal percepatan dan gesekan
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    void Update() 
    {
        Move(); 
    }

    public void Move()
    {
        // Mendapatkan arah gerakan dari input player
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection != Vector2.zero)
        {
            // Menghitung kecepatan target berdasarkan arah gerakan dan kecepatan maksimum
            Vector2 targetVelocity = moveDirection * maxSpeed;
            // Menggerakkan player menuju kecepatan target
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, moveVelocity.magnitude * Time.deltaTime);
        }
        else
        {
            // Menghitung gaya gesekan saat berhenti
            Vector2 frictionForce = GetFriction();
            // Menghentikan player secara bertahap
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, frictionForce.magnitude * Time.deltaTime);

            // Jika kecepatan player kurang dari nilai stopClamp, berhenti total
            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public Vector2 GetFriction()
    {
        // Jika ada input (player bergerak), gunakan moveFriction untuk memperlambat gerakan
        if (moveDirection != Vector2.zero)
        {
            return new Vector2(
                moveDirection.x != 0 ? moveFriction.x : 0,
                moveDirection.y != 0 ? moveFriction.y : 0
            );
        }
        // Jika tidak ada input, gunakan stopFriction untuk menghentikan player
        else 
        {
            return new Vector2(
                rb.velocity.x != 0 ? stopFriction.x : 0,
                rb.velocity.y != 0 ? stopFriction.y : 0
            );
        }
    }

    public bool IsMoving()
    {
        // Mengembalikan true jika player sedang bergerak
        return rb.velocity != Vector2.zero;
    }
}
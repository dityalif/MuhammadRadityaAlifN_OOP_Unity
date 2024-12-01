using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed = new(2f, 2f);
    [SerializeField] Vector2 timeToStop = new(2.5f, 2.5f);
    [SerializeField] Vector2 stopClamp;

    private Vector2 moveDirection; 
    private Vector2 moveVelocity;  
    private Vector2 moveFriction;  
    private Vector2 stopFriction;  
    private Rigidbody2D rb;        

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float spriteWidth;
    private float spriteHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);

        Vector3 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));
        minBounds = new Vector2(minScreenBounds.x + 0.3f, minScreenBounds.y + 0.2f);
        maxBounds = new Vector2(maxScreenBounds.x - 0.3f, maxScreenBounds.y - 0.5f);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x / 2;
        spriteHeight = spriteRenderer.bounds.size.y / 2;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection != Vector2.zero)
        {
            Vector2 targetVelocity = moveDirection * maxSpeed;
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, moveVelocity.magnitude * Time.deltaTime);
        }
        else
        {
            Vector2 frictionForce = GetFriction();
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, frictionForce.magnitude * Time.deltaTime);
        }

        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, minBounds.x + spriteWidth, maxBounds.x - spriteWidth),
            Mathf.Clamp(transform.position.y, minBounds.y + spriteHeight, maxBounds.y - spriteHeight)
        );
        transform.position = clampedPosition;
    }

    public Vector2 GetFriction()
    {
        if (moveDirection != Vector2.zero)
        {
            return new Vector2(
                moveDirection.x != 0 ? moveFriction.x : 0,
                moveDirection.y != 0 ? moveFriction.y : 0
            );
        }
        return new Vector2(
            rb.velocity.x != 0 ? stopFriction.x : 0,
            rb.velocity.y != 0 ? stopFriction.y : 0
        );
    }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}

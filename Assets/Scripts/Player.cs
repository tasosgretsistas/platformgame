using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true; // Determines the direction the player is facing.
    [HideInInspector]
    public bool jump = false; // Determines if the player will jump.
    [HideInInspector]
    public bool alive = true; // Determines if the player is alive.
    [HideInInspector]
    public bool invulnerable = false;

    // The point at which the player is considered to have fallen off the screen, and is thus dead.
    public float fallOffScreenYPoint = -0.53f; 

    public int lives = 3; // The player's remaining lives.
    public int maxHealth = 10;
    public int currentHealth = 10; // The player's remaining hit points.

    public float invulnerabilityAfterHit = 1f;
    private float invulnerabilityTimer = 0f;

    public float moveForce = 8f; // The force applied to move the player horizontally.
    public float maxSpeed = 5f; // The player's maximum velocity.

    public float jumpForce = 400f; // The force applied to the player while he's jumping.
    
    private bool grounded = false; // Determines whether the player is currently standing on ground.

    // Self-reference to the player's rigidbody component.
    private Rigidbody2D rb;

    // Self-reference to the player's groundCheck transform.
    private Transform groundCheck;

    // Initialization.
    void Awake()
    {
        // Flips the player horizontally as the sprite faces left by default.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
    }    

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            if (Input.GetButtonDown("Jump") && grounded)
                jump = true;            
        }

        if (transform.position.y < fallOffScreenYPoint)
        {
            alive = false;
        }

        if (invulnerable)
        {
            if (invulnerabilityTimer < invulnerabilityAfterHit)
                invulnerabilityTimer += Time.deltaTime;

            else
            {
                invulnerable = false;
                invulnerabilityTimer = 0f;
            }
        }        
    }

    void FixedUpdate()
    {
        if (alive)
        {
            // Cache the horizontal input.
            float h = Input.GetAxis("Horizontal");

            // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            if (h * rb.velocity.x < maxSpeed)
                // ... add a force to the player.
                rb.AddForce(Vector2.right * h * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (rb.velocity.x > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (h > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (h < 0 && facingRight)
                // ... flip the player.
                Flip();

            if (jump)
            {
                // Add a vertical force to the player.
                rb.AddForce(new Vector2(0f, jumpForce));

                // Make sure the player can't jump again until the jump conditions from Update are satisfied.
                jump = false;
            }
        }
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage()
    {
        if (!invulnerable)
        {          
            if (alive)
            {
                this.currentHealth--;
                this.invulnerable = true;

                if (currentHealth <= 0)
                {
                    this.alive = false;
                    this.lives--;
                }
            }
        }
    }
}

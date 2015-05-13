using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hero : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true; // Determines the direction the hero is facing.
    [HideInInspector]
    public bool jump = false; // Determines if the hero will jump.
    [HideInInspector]
    public bool alive = true; // Determines if the hero is alive.
    [HideInInspector]
    public bool invulnerable = false; // Determines if the hero is vulnerable to attacks.

    // The point at which the hero is considered to have fallen off the screen, and is thus dead.
    public float fallOffScreenYPoint = -0.53f;

    // The hero's remaining lives. This is static because it should not reset when the level resets.
    public static int lives = 3;

    public int maxHealth = 5; // The hero's maximum amount of health points.
    public int currentHealth = 5; // The hero's remaining health points.

    // The amount of time that the hero is invulnerable for after being hit by an enemy.
    public float invulnerabilityAfterHit = 1f;
    // The amount of time the hero has before becoming vulnerable again. When this becomes equal to invulnerabilityAfterHit, the hero becomes vulnerable again.
    private float invulnerabilityTimer = 0f;

    public float moveForce = 8f; // The force applied to move the hero horizontally.
    public float maxSpeed = 5f; // The hero's maximum velocity in either direction.

    public float jumpForce = 400f; // The force applied to the hero while he's jumping.

    private bool grounded = false; // Determines whether the hero is currently standing on ground.

    // Self-reference to the hero's rigidbody component.
    private Rigidbody2D rb;

    // Self-reference to the hero's ground check transforms.
    private Transform groundCheck;
    private Transform center;

    // Initialization.
    void Awake()
    {
        // Initialize the self-reference components.
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
        center = transform.Find("center");
    }    

    // Update is called once per frame
    void Update()
    {
        // If the hero falls lower than the fallOffScreenYPoint...
        if (transform.position.y < fallOffScreenYPoint)
        {
            // ... he dies.
            alive = false;
        }

        // Actions that the hero can only perform while alive: jumping.
        if (alive)
        {
            // This checks to see if the hero is currently grounded by casting a line between the groundCheck transform and the hero's center.
            // If this line is intercepted by any objects that have the "Ground" layer, then the hero is considered grounded.
            grounded = Physics2D.Linecast(center.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            // If the player presses the jump button and the hero is currently grounded...
            if (Input.GetButtonDown("Jump") && grounded)
                // ... jump is set to true, which causes the hero to jump in FixedUpdate.
                jump = true;            
        }

        // If the hero's invulnerable flag is true...
        if (invulnerable)
        {
            // If the invulnerability timer is smaller than the maximum invulnerability timer...
            if (invulnerabilityTimer < invulnerabilityAfterHit)
                // ... it keeps counting.
                invulnerabilityTimer += Time.deltaTime;

            else
            {
                // Otherwise, the timer is reset to 0 and the invulnerable flag is set to false.
                invulnerable = false;
                invulnerabilityTimer = 0f;
            }
        }        
    }

    void FixedUpdate()
    {
        if (alive)
        {
            // The following code was adapted from the "Unity 2D Tutorial" tutorial game code, with slight modifications.

            // Cache the horizontal input.
            float h = Input.GetAxis("Horizontal");

            // If the hero is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            if (h * rb.velocity.x < maxSpeed)
                // ... add a force to the player.
                rb.AddForce(Vector2.right * h * moveForce);

            // If the hero's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                // ... set the hero's velocity to the maxSpeed in the x axis.
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

            // If the input is moving the hero right and the hero is facing left...
            if (h > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the hero left and the hero is facing right...
            else if (h < 0 && facingRight)
                // ... flip the player.
                Flip();

            if (jump)
            {
                // Add a vertical force to the hero.
                rb.AddForce(new Vector2(0f, jumpForce));

                // Make sure the hero can't jump again until the jump conditions from Update are satisfied.
                jump = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // If the hero is collididng with a "PlatformSpecial" tagged object...
        if (collision.collider.tag == "PlatformSpecial")
            // ... parent the hero to the object, for a smoother animation.
            this.transform.parent = collision.collider.transform;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        // Then, when the hero exits any collisions, unparent it from everything.
        this.transform.parent = null;
    }

    void Flip()
    {
        // Switch the way the hero is labelled as facing
        facingRight = !facingRight;

        // Multiply the hero's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage()
    {
        // If the hero is not invulnerable...
        if (!invulnerable)
        {
            // ... and if he is alive...
            if (alive)
            {
                // ... he loses 1 point of health and becomes invulnerable.
                this.currentHealth--;
                this.invulnerable = true;

                // If the hero's life is less than or equal to 0...
                if (currentHealth <= 0)
                {
                    // ... he dies.
                    this.alive = false;
                }
            }
        }
    }
}

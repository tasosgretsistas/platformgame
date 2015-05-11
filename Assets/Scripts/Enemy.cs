using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	// The player's object.
	public Player player;

    protected bool facingRight = false;

    protected Rigidbody2D rb;

    public int health = 1;
    public float speed = 0.02f;

    // The distance at which the NPC can perceive the player.
    public float aggroRange = 5f;

	// The health at which the NPC will choose to run away instead of chase the player.
    public int fleeThreshold = 0;

    public virtual void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
    public virtual void FixedUpdate() 
	{
		if (DistanceToPlayer(player.transform) <= aggroRange)
        {
            // If the player is within aggro range and this enemy is healthy, it pursues the player.
            if (this.health >= this.fleeThreshold)
                this.MoveTowardsPlayer();
            // Otherwise it runs away.
            else
                this.MoveAwayFromPlayer();
        }
	}

	// Returns the distance to the player in the form of a float in absolute value form.
	public virtual float DistanceToPlayer(Transform playerPosition)
	{
		return (Mathf.Abs(this.transform.position.x - this.player.transform.position.x)) + (Mathf.Abs(this.transform.position.y - this.player.transform.position.y)) ;
	}

	// Makes the enemy move towards the player.
    public virtual void MoveTowardsPlayer()
	{
		if (this.transform.position.x > this.player.transform.position.x)
			this.transform.position -= new Vector3 (speed, 0, 0);
		else if (this.transform.position.x < player.transform.position.x)
			this.transform.position += new Vector3 (speed, 0, 0);
	}

    // Makes the enemy move away from the player.
    public virtual void MoveAwayFromPlayer()
	{
		if (this.transform.position.x > this.player.transform.position.x)
			this.transform.position += new Vector3 (speed, 0, 0);
		else if (this.transform.position.x < this.player.transform.position.x)
			this.transform.position -= new Vector3 (speed, 0, 0);
	}

    public void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

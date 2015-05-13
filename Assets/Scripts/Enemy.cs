using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    // The hero object that this enemy tracks.
	public Hero hero;
    
    [HideInInspector]
    public bool facingRight = false; // Determines if the enemy is facing right.

    public int health = 1; // The enemy's current health points.
    public int maxHealth = 1; // The enemy's maximum health points.
    public float speed = 0.02f;  // The enemy's speed for moving with the default move methods.

    // The distance at which the enemy can perceive the hero.
    public float aggroRange = 5f;

    // The health at which the enemy will choose to run away instead of chase the hero.
    public int fleeThreshold = 0;
    
    // Self-reference to the enemy's Rigidbody2D component.
    protected Rigidbody2D rb;

    public virtual void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This is the default FixedUpdate function for the base Enemy class. 
    /// By default, it just moves the enemy towards or away from the hero, depending on the enemy's health.
    /// It should be overriden by every specific enemy to be provide custom behaviour.
    /// </summary>
    public virtual void FixedUpdate() 
	{
        // If the hero is within the enemy's aggro range...
		if (DistanceToHero(hero.transform) <= aggroRange)
        {
            // If this enemy is healthy...
            if (this.health >= this.fleeThreshold)
                // ... it pursues the hero.
                this.MoveTowardsPlayer();

            // Otherwise it runs away.
            else
                this.MoveAwayFromPlayer();
        }
	}

    /// <summary>
    /// Returns the distance to a hero in the form of a float in absolute value form.
    /// </summary>
    /// <param name="heroPosition">The hero's position - should be the hero's transform.position</param>
    /// <returns></returns>
	public virtual float DistanceToHero(Transform heroPosition)
	{
		return (Mathf.Abs(this.transform.position.x - hero.transform.position.x)) + (Mathf.Abs(this.transform.position.y - hero.transform.position.y)) ;
	}

	/// <summary>
    /// Makes the enemy move towards the hero by modifying its position directly.
    /// This is only for testing purposes and each enemy should have its individual movememt method.
	/// </summary>
    public virtual void MoveTowardsPlayer()
    {
        this.transform.position += new Vector3(Mathf.Sign(hero.transform.position.x - this.transform.position.x) * speed, 0, 0);
    }

    /// <summary>
    /// Makes the enemy move away from the hero by modifying its position directly.
    /// This is only for testing purposes and each enemy should have its individual movememt method.
    /// </summary>
    public virtual void MoveAwayFromPlayer()
	{
        this.transform.position -= new Vector3(Mathf.Sign(hero.transform.position.x - this.transform.position.x) * speed, 0, 0);
	}

    public void Flip()
    {
        // Switch the way the enemy is labelled as facing
        facingRight = !facingRight;

        // Multiply the enemy's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

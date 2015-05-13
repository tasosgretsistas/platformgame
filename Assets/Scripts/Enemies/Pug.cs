using UnityEngine;
using System.Collections;

public class Pug : Enemy
{
    public float hopForce = 200f;       // The force added to the Pug when it hops.
    public float maxHopLength = 2.5f;   // The maximum amount of distance that the a hop can cover in the X axis.
    public float hopDelay = 2f;         // The delay between hops.

    private float hopTimer = 0f;        // The amount of time since the last hop.

    public override void FixedUpdate()
    {
        // If the time since the Pug's last hop is equal to or larger than the hop delay, AND
        // if its distance to the hero is equal to or smaller than its aggro range...
        if (hopTimer >= hopDelay && DistanceToHero(hero.transform) <= aggroRange)
            // ... it hops towards the hero.
            HopTowardsPlayer();
        else
            // Otherwise, the hop timer keeps counting.
            hopTimer += Time.fixedDeltaTime;

    }

    /// <summary>
    /// Hops towards the hero by adding a force on both the X and Y axis.
    /// </summary>
    void HopTowardsPlayer()
    {
        // This variable will be used to track the distance that the hop will cover.
        // By default, it is equal to the maxHopLength variable.
        float hopLength = maxHopLength;

        // The relative distance to the hero in the X axis alone.
        float relativeXToHero = this.transform.position.x - hero.transform.position.x;

        // If the Pug would jump right and it isn't facing right...
        if (relativeXToHero < 0 && !this.facingRight)
            // ... it is flipped.
            Flip();
        
        // Else, if Pug would jump left and it is facing right...
        else if (relativeXToHero > 0 && this.facingRight)
            // ... it is flipped.
            Flip();

        float distanceToHero = DistanceToHero(hero.transform);

        // If the distance to the hero is smaller than the maxHopLength...
        if (distanceToHero < hopLength)
            // ... the hop length will be equal to the distance to the hero.
            hopLength = distanceToHero;


        // Finally, the Pug hops using this formula: 
        this.rb.AddForce(new Vector2(-Mathf.Sign(relativeXToHero) * hopLength * hopForce / 5, hopForce));
        this.hopTimer = 0f;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // If the Pug collides with an object and that object's tag is "Hero" ...
        if (collision.collider.tag == "Hero")
        {
            // ... the hero takes damage.
            hero.TakeDamage();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Pug : Enemy 
{
    public float hopForce = 200f;
    public float maxHopLength = 2.5f;    
    public float hopDelay = 2f;

    private float hopTimer = 0f;
	
	public override void FixedUpdate () 
    {
        hopTimer += Time.fixedDeltaTime;

        if (hopTimer >= hopDelay && DistanceToPlayer(player.transform) <= aggroRange)
            HopTowardsPlayer();
	}

    void HopTowardsPlayer()
    {
        float hopLength = maxHopLength;

        float relativeXToPlayer = this.transform.position.x - player.transform.position.x;

        if (relativeXToPlayer < 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (relativeXToPlayer > 0 && facingRight)
            // ... flip the player.
            Flip();

        if (Mathf.Abs(relativeXToPlayer) < hopLength)
            hopLength = Mathf.Abs(relativeXToPlayer);
        
        this.rb.AddForce(new Vector2(-Mathf.Sign(relativeXToPlayer) * hopLength * hopForce / 5, this.hopForce));
        this.hopTimer = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //OnCollisionEnter2D(collision);

        if (collision.collider.tag == "Hero")
        {
            player.TakeDamage();
        }
    }
}

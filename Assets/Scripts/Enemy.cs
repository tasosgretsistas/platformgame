using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	// The player's object.
	public GameObject player;

    public int health = 10;
    public float speed = 0.02f;

    // The distance at which the NPC can perceive the player.
    public float aggroRange = 10.0f;

	// The health at which the NPC will choose to run away instead of chase the player.
	private int fleeThreshold = 5;	

	// Update is called once per frame
	void Update () 
	{
		if (DistanceToPlayer(player) <= aggroRange)
        {
            // If the player is within aggro range and this enemy is healthy, it pursues the player.
            if (health >= fleeThreshold)
                MoveTowardsPlayer();
            // Otherwise it runs away.
            else
                MoveAwayFromPlayer();
        }
	}

	// Returns the distance to the player in the form of a float in absolute value form.
	float DistanceToPlayer(GameObject player)
	{
		return (Mathf.Abs(this.transform.position.x - player.transform.position.x)) + (Mathf.Abs(this.transform.position.y - player.transform.position.y)) ;
	}

	// Makes the NPC move towards the player.
	void MoveTowardsPlayer()
	{
		if (this.transform.position.x > player.transform.position.x)
			this.transform.position -= new Vector3 (speed, 0, 0);
		else if (this.transform.position.x < player.transform.position.x)
			this.transform.position += new Vector3 (speed, 0, 0);
	}

	// Makes the NPC move away from the player.
	void MoveAwayFromPlayer()
	{
		if (this.transform.position.x > player.transform.position.x)
			this.transform.position += new Vector3 (speed, 0, 0);
		else if (this.transform.position.x < player.transform.position.x)
			this.transform.position -= new Vector3 (speed, 0, 0);
	}
}

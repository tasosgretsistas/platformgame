using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	// The player's object.
	public GameObject player;

	public int health;
	public float speed;

	// The health at which the NPC will choose to run away instead of chase the player.
	private int fleeThreshold = 5;

	// The distance at which the NPC can perceive the player.
	private float aggroRange = 10.0f;

	// Update is called once per frame
	void Update () 
	{
		// This is a hack that prevents the player from ever rotating because I couldn't find a more elegant solution.
		this.transform.rotation = new Quaternion (0, 0, 0, 0);

		Behaviour ();
	}

	// Returns the distance to the player in the form of a float in absolute value form.
	float DistanceToPlayer(GameObject player)
	{
		return Mathf.Abs(this.transform.position.x - player.transform.position.x);
	}

	void Behaviour()
	{
		// If the player is within aggro range and this enemy is healthy, it pursues the player.
		if (DistanceToPlayer (player) <= aggroRange && health >= fleeThreshold)
			MoveTowardsPlayer ();

		// Otherwise it runs away.
		else if (DistanceToPlayer (player) <= aggroRange && health <= fleeThreshold)
			MoveAwayFromPlayer ();
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

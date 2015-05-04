using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public GameObject player;

	public int health;
	public float speed;

	private int fleeThreshold = 5;
	private float aggroRange = 10.0f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		Behaviour ();
	}

	// Returns the distance to the player in the form of a float in absolute value form.
	float DistanceToPlayer(GameObject player)
	{
		return Mathf.Abs(this.transform.position.x - player.transform.position.x);
	}

	void Behaviour()
	{
		if (DistanceToPlayer (player) <= aggroRange && health >= fleeThreshold)
			MoveTowardsPlayer ();
		else if (DistanceToPlayer (player) <= aggroRange && health <= fleeThreshold)
			MoveAwayFromPlayer ();
	}

	void MoveTowardsPlayer()
	{
		if (this.transform.position.x > player.transform.position.x)
			this.transform.position -= new Vector3 (speed, 0, 0);
		else if (this.transform.position.x < player.transform.position.x)
			this.transform.position += new Vector3 (speed, 0, 0);
	}

	void MoveAwayFromPlayer()
	{
		if (this.transform.position.x > player.transform.position.x)
			this.transform.position += new Vector3 (speed, 0, 0);
		else if (this.transform.position.x < player.transform.position.x)
			this.transform.position -= new Vector3 (speed, 0, 0);
	}
}

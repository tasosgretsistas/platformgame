using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed;
	public float maxSpeed;
	public float jumpHeight;

	bool isFalling = false;

	// Self-reference to the player's rigidbody component.
	private Rigidbody2D rb;

	public bool alive;
	
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		alive = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// This is a hack that prevents the player from ever rotating because I couldn't find a more elegant solution.
		this.transform.rotation = new Quaternion (0, 0, 0, 0);


		if (alive) 
		{
			// This is what causes the player to move. However the code is bad and should be changed to the 
			// commented out segment below if you can figure out why the fuck it's not working.
			//this.transform.position += new Vector3 (moveHorizontal, 2 * moveVertical, 0) * speed;

			//Vector2 movement = new Vector2 (moveHorizontal * speed, moveVertical * 10);
		
			if (Input.GetKey(KeyCode.LeftArrow) && rb.velocity.x >= -maxSpeed)
			    rb.velocity += new Vector2(-0.15f, 0);

			if (Input.GetKey(KeyCode.RightArrow) && rb.velocity.x <= maxSpeed)
			    rb.velocity += new Vector2(0.15f, 0);

			if (Input.GetButtonDown ("Jump") && isFalling == false)
			{
				Jump();
			}
		}

		if (this.transform.position.y < -10) 
		{
			alive = false;
		}
	}

	void Jump()
	{
		rb.velocity += new Vector2 (0, jumpHeight);
	}

	void OnCollisionStay()
	{
		isFalling = false;
	}

}

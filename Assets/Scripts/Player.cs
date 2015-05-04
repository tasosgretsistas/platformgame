using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour 
{
	// Used to show debug information.
	public Text debugText;

	public Text deathText;

	public float speed;

	// Self-reference to the player's rigidbody component.
	private Rigidbody2D rb;

	private bool alive;
	
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		alive = true;
		deathText.text = "";
	}
	
	// Update is called once per frame
	void Update () 
	{
		// This is a hack that prevents the player from ever rotating because I couldn't find a more elegant solution.
		this.transform.rotation = new Quaternion (0, 0, 0, 0);

		// Input received from the player is stored in these two floats.
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Mathf.Clamp (Input.GetAxis ("Vertical"), 0, 0.5f);

		//Debug information.
		debugText.text = "X: " + moveHorizontal + "\nY: " + moveVertical;

		if (alive) 
		{
			// This is what causes the player to move. However the code is bad and should be changed to the 
			// commented out segment below if you can figure out why the fuck it's not working.
			this.transform.position += new Vector3 (moveHorizontal, 2 * moveVertical, 0) * speed;

			//Vector2 movement = new Vector2 (moveHorizontall, moveVertical);
		
			//rb.AddForce (movement);
		}

		if (this.transform.position.y < -10) 
		{
			alive = false;
			deathText.text = "YOU DIED LIKE A BITCH";
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Text debugText;

	public float moveX;
	public float moveY;

	public float velocity;
	public float maxVelocity;

	private Rigidbody2D rb;
	
	private Vector3 startingPosition;

	private float endXPosition;
	private float endYPosition;

	private bool finishedXMovement;
	private bool finishedYMovement;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();

		startingPosition = this.transform.position;

		endXPosition = startingPosition.x + moveX;
		endYPosition = startingPosition.y + moveY;

		finishedXMovement = false;
		finishedYMovement = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		debugText.text = "Starting position: ( " + startingPosition.x + ", " + startingPosition.y + ")\n";
		debugText.text += "Final position: ( " + endXPosition+ ", " + endYPosition + ")\n";
		debugText.text += "Current position: ( " + this.transform.position.x + ", " + this.transform.position.y  + ")\n";


		rb.MovePosition (new Vector3(this.transform.position.x + moveX, this.transform.position.y + moveY) * velocity * Time.fixedDeltaTime);

		//MoveY ();

	}

	void SetDestination()
	{
		SetDestination = 
	}

	void MoveX()
	{
		float direction = moveX / Mathf.Abs (moveX);


		if (!finishedXMovement)
		{

		if (this.transform.position.x != endXPosition)
		{
			if (Mathf.Abs (rb.velocity.x) + velocity < maxVelocity)
				rb.velocity += new Vector2(velocity * direction, 0);

			else
				rb.velocity += new Vector2(maxVelocity, 0);
		}

		else if (this.transform.position.x == startingPosition.x + moveX)
			{}
		}
	}

	void MoveY()
	{
		float direction = moveY / Mathf.Abs (moveY);
		float endPosition = endYPosition;
		
		if (finishedYMovement) 
		{
			direction *= -1;
			endPosition = -startingPosition.y;
		}			

		if (Mathf.Abs (this.transform.position.y) > Mathf.Abs(endPosition)) 
			{

				if (Mathf.Abs (rb.velocity.y) + velocity < maxVelocity) {
					rb.velocity += new Vector2 (0, velocity * direction);
					debugText.text += "Y Velocity: " + rb.velocity.y + " (Accelarating)";


				} 
				else 
				{
					rb.velocity = new Vector2 (0, maxVelocity * direction);
					debugText.text += "Y Velocity: " + rb.velocity.y + " (Accelerated to max)";
				}
			} 

			else if (!finishedYMovement)
			{
				finishedYMovement = true;
				rb.velocity = new Vector2 (0, 0);
				debugText.text += "Y Velocity: " + rb.velocity.y + " (Stopped)";
			}

		else
		{			
			finishedYMovement = false;
			rb.velocity = new Vector2 (0, 0);
			debugText.text += "Y Velocity: " + rb.velocity.y + " (Stopped)";
		}
		 
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed;
	public float maxSpeed;
	public float jumpHeight;

    private bool facingRight = true;

	bool isFalling = false;

	// Self-reference to the player's rigidbody component.
	private Rigidbody2D rb;

	public bool alive;
	
	// Use this for initialization
	void Start () 
	{
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z) ;
		rb = GetComponent<Rigidbody2D> ();
		alive = true;
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Hax
		this.rb.velocity = new Vector2(this.rb.velocity.x, Mathf.Clamp (this.rb.velocity.y, -14, 7));

		if (alive) 
		{
			// This is what causes the player to move. However the code is bad and should be changed to the 
			// commented out segment below if you can figure out why the fuck it's not working.
			//this.transform.position += new Vector3 (moveHorizontal, 2 * moveVertical, 0) * speed;

			//Vector2 movement = new Vector2 (moveHorizontal * speed, moveVertical * 10);


            if (Input.GetKey(KeyCode.LeftArrow) && rb.velocity.x >= -maxSpeed)
            {
                rb.velocity += new Vector2(-0.15f, 0);

                if (facingRight)
                    Flip();
                
            }

            if (Input.GetKey(KeyCode.RightArrow) && rb.velocity.x <= maxSpeed)
            {
                rb.velocity += new Vector2(0.15f, 0);

                if (!facingRight)
                    Flip();
            }

			if (Input.GetButtonDown ("Jump") && isFalling == false)
			{
				Jump();
			}
		}

		if (this.transform.position.y < -0.53) 
		{
			alive = false;
		}
	}

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	public Player player;
	private Rigidbody2D playerRigidbody;

	public Text debugText;
	public Text deathText;

	// Use this for initialization
	void Start () 
	{
		playerRigidbody = player.GetComponent<Rigidbody2D>();
		deathText.text = "";
	}
	
	// Update is called once per frame
	void Update () 
	{
		debugText.text = DisplayPlayerVelocity ();

		if (player.alive) {
			deathText.text = "";
		} else {
			deathText.text = "YOU DIED LIKE A BITCH";
		}
	}

	string DisplayPlayerVelocity()
	{

		return "X: " + playerRigidbody.velocity.x +
			 "\nY: " + playerRigidbody.velocity.y;
	}	
}

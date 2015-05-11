using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	public Player player;
	private Rigidbody2D playerRigidbody;

	public Text debugText;
    public Text statusText;
	public Text deathText;

	// Use this for initialization
	void Awake () 
	{
		playerRigidbody = player.GetComponent<Rigidbody2D>();

		deathText.text = "";

        statusText.text = "Lives: " + player.lives +
                          "\nHealth: " + player.currentHealth + "/" + player.maxHealth;

        statusText.text = DisplayPlayerHealth();
	}
	
	// Update is called once per frame
	void Update () 
	{
		debugText.text = DisplayPlayerVelocity ();

        statusText.text = DisplayPlayerHealth();

		if (player.alive) 
        {
			deathText.text = "";
		} 

        else 
        {
			deathText.text = "YOU DIED LIKE A BITCH";
		}
	}

    string DisplayPlayerHealth()
    {
        return "Health: " + player.currentHealth + "/" + player.maxHealth +
             "\nLives: " + player.lives;
    }

	string DisplayPlayerVelocity()
	{
		return "X: " + playerRigidbody.velocity.x +
			 "\nY: " + playerRigidbody.velocity.y;
	}	
}

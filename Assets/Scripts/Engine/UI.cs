using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour 
{
    public Hero hero;                       // The hero whose stats to track.
    private Rigidbody2D heroRigidbody;    // Quick reference to the hero's Rigidbody2D component.

	public Text debugText;                  // Debugging text helpful to the game's development. Disable when done.
    public Text statusText;                 // The status text - this will contain the hero's health and remaining lives.
    public Text deathText;                  // The death text - triggered when the hero dies.

	// Use this for initialization
	void Awake () 
	{
		heroRigidbody = hero.GetComponent<Rigidbody2D>();

		deathText.text = "";

        statusText.text = DisplayHeroStatus();
	}
	
	// Update is called once per frame
    void Update()
    {
        debugText.text = DisplayHeroVelocity();

        statusText.text = DisplayHeroStatus();

        deathText.text = DisplayDeathText();
    }
		
    /// <summary>
    /// Displays the hero's status - his health and life parameters.
    /// </summary>
    /// <returns>Formatted string.</returns>
    string DisplayHeroStatus()
    {
        return "Health: " + hero.currentHealth + "/" + hero.maxHealth +
             "\nLives: " + Hero.lives;
    }

    /// <summary>
    /// Display's the hero's X and Y velocity.
    /// </summary>
    /// <returns>Formatted string.</returns>
	string DisplayHeroVelocity()
	{
		return "X: " + heroRigidbody.velocity.x +
			 "\nY: " + heroRigidbody.velocity.y;
	}
	
    /// <summary>
    /// Displays the death text, such as "You died!" and "Game over!"
    /// </summary>
    /// <returns>Formatted string.</returns>
    string DisplayDeathText()
    {
        if (hero.alive)
        {
            return "";
        }

        else if (Hero.lives >= 1)
        {
            return "YOU DIED LIKE A BITCH";
        }

        else
            return "GAME OVER";
    }
}

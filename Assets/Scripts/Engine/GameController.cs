using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    // The game's main hero object.
	public Hero hero;

	public Transform spawnPoint;        // The original spawn point.
    private Transform respawnPoint;     // The point at which the hero respawns after death.

    public float respawnDelay = 3f;     // The amount of time before the hero respawns.
    private float respawnTimer = 0f;    // The time left until the hero respawns when dead.

	void Awake () 
	{
        // The hero's position becomes that of his original spawn position.
		hero.transform.position = spawnPoint.position;

        // At the start of the game, the respawn point is the hero's original spawn point.
        respawnPoint = spawnPoint;
	}
	
	// Update is called once per frame
    void Update()
    {
        // If the hero is dead...
        if (!hero.alive)
            // ... the respawn timer starts counting.
            respawnTimer += Time.deltaTime;

        // If the respawn timer is larger than the respawn delay...
        if (respawnTimer >= respawnDelay)
            // The respawn routine takes place.
            RespawnPlayer();
    }

	/// <summary>
	/// Respawns the player and resets the player when applicable.
	/// </summary>
	void RespawnPlayer()
	{
        // First, the respawn timer is set to 0.
		respawnTimer = 0;

        // Then, if the hero has at least 1 life left...
        if (Hero.lives >= 1)
        {
            // ... he respawns and the level resets.
            hero.alive = true;
            Hero.lives--;
            Application.LoadLevel("Main");
        }

        else
        {
            // Otherwise, the game is over.

            // Insert game over code here.
        }
	}
}

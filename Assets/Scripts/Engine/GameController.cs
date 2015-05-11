using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public Player player;

	public Transform spawnPoint;
    private Transform respawnPoint;

    public float respawnDelay = 3f;
    private float respawnTimer = 0f;

	// Use this for initialization
	void Awake () 
	{
		//The player spawns at the start of the game.
		player.transform.position = spawnPoint.position;
        respawnPoint = spawnPoint;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the player is dead, the respawn timer begins to tick for the respawn routine to take place.
		if (!player.alive) 
		{
			respawnTimer += Time.deltaTime;
		}

		if (respawnTimer >= respawnDelay) 
		{
			RespawnPlayer();
		}
	}

	/// <summary>
	/// Respawns the player.
	/// </summary>
	void RespawnPlayer()
	{
		respawnTimer = 0;
		player.alive = true;
        player.currentHealth = player.maxHealth;
        player.transform.position = respawnPoint.position;
	}
}

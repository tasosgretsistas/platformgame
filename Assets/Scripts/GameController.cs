using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	public GameCamera gameCamera;

	public Transform spawnPoint;
	 
	public float respawnDelay;
	private float respawnTimer;
	private float defaultRespawnDelay = 3;

	// Use this for initialization
	void Start () 
	{
		//The player spawns at the start of the game.
		player.transform.position = spawnPoint.position;

		//If the developer forgot to set the delay timer, it is set to a default value for him.
		if (respawnDelay == 0)
			respawnDelay = defaultRespawnDelay;
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
			RespawnPlayer ();
		}
	}

	/// <summary>
	/// Respawns the player.
	/// </summary>
	void RespawnPlayer()
	{
		respawnTimer = 0;
		player.alive = true;
		player.transform.position = spawnPoint.position;
		gameCamera.ResetCamera();
	}
}

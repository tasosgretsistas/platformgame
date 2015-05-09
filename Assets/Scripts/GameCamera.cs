using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
	public Player player;

	public float xOffset = 2;
	public float yOffset = 2;

	public float cameraStopFollowingPoint = -4.5f;

	//The constant distance between the camera and the player
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		this.transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, this.transform.position.z);

		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () 
	{
		//This if check is here to prevent the camera from following the player to the depths of hell
		 if (player.alive)
		{
			transform.position = new Vector3(player.transform.position.x + offset.x, 
		                                 Mathf.Clamp ((player.transform.position.y + offset.y), cameraStopFollowingPoint, 15),
		                                 	 player.transform.position.z + offset.z);
		}
	}

	public void ResetCamera()
	{
		transform.position = player.transform.position + offset;	
	}
}

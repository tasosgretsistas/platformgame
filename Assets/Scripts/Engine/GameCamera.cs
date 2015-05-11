using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
	public Player player;

	public float xOffset = 1.5f;
	public float yOffset = 1.5f;

    public float topStopFollowingPoint = 15f;
	public float bottomStopFollowingPoint = 5f;
    public float leftStopFollowingPoint = 8.9f;
    public float rightStopFollowingPoint = 1080f;

	//The constant distance between the camera and the player
	private Vector3 offset;

	// Use this for initialization
	void Awake() 
	{
		this.transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, this.transform.position.z);

		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate() 
	{
		//This if check is here to prevent the camera from following the player to the depths of hell
		 if (player.alive)
		{
			transform.position = new Vector3(Mathf.Clamp((player.transform.position.x + offset.x), leftStopFollowingPoint, rightStopFollowingPoint),
                                             Mathf.Clamp((player.transform.position.y + offset.y), bottomStopFollowingPoint, topStopFollowingPoint),
		                                 	 player.transform.position.z + offset.z);
		}
	}
}

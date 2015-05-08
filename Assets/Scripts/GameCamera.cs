using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
	public GameObject player;

	public float cameraStopFollowingPoint;
	private float defaultStopFollowingPoint = -7.5f;

	//The constant distance between the camera and the player
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;

		//If the developer forgot to set the camera's stop following point, it is set to a default value for him.
		if (cameraStopFollowingPoint == 0)
			cameraStopFollowingPoint = defaultStopFollowingPoint;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		//This if check is here to prevent the camera from following the player to the depths of hell
		if (player.transform.position.y > cameraStopFollowingPoint)
			transform.position = player.transform.position + offset;	
	}
}

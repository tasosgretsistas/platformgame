using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour 
{
	public GameObject player;

	//The constant distance between the camera and the player
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		//This if check is here to prevent the camera from following the player to the depths of hell
		if (transform.position.y > -5)
			transform.position = player.transform.position + offset;	
	}
}

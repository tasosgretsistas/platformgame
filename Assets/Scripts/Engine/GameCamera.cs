using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
    // The hero object that the camera will follow.
	public Hero hero;

    // The distance from the hero that the camera will center at.
	public float xOffset = 1.5f;
	public float yOffset = 1.5f;

    // The points at which the camera will stop following the hero off the screen.
    public float topStopFollowingPoint = 15f;
	public float bottomStopFollowingPoint = 5f;
    public float leftStopFollowingPoint = 8.9f;
    public float rightStopFollowingPoint = 1080f;

	void LateUpdate() 
	{
         // If the hero is alive...
		 if (hero.alive)
		{
            // ... the camera's position becomes that of the hero's plus the specified offsets.
            transform.position = new Vector3(Mathf.Clamp((hero.transform.position.x + xOffset), leftStopFollowingPoint, rightStopFollowingPoint),
                                             Mathf.Clamp((hero.transform.position.y + yOffset), bottomStopFollowingPoint, topStopFollowingPoint),
		                                 	 transform.position.z);		}
	}
}

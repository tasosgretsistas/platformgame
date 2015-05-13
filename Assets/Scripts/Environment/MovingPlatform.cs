using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour 
{
    // The movement types that the platform can use.
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}

    public FollowType Type = FollowType.MoveTowards; // Determines which movement type the platform will use.
	public Path Path;                                // The path that the platform will follow.
	public float Speed = 1;                          // The speed that the platform moves at.
	public float MaxDistanceToGoal = 0.1f;           // The maximum distance to the next point offset before the platform starts moving to the next point in the path.

	private IEnumerator<Transform> currentPoint;

	public void Start()
	{
		if (Path == null)
		{
			Debug.LogError ("A path cannot be null.", gameObject);
			return;
		}

		currentPoint = Path.GetPathEnumerator();

		currentPoint.MoveNext();

		if (currentPoint.Current == null)
			return;

		transform.position = currentPoint.Current.position;
	}

	public void Update()
	{
		if (currentPoint == null || currentPoint.Current == null)
			return;

		if (Type == FollowType.MoveTowards)
			transform.position = Vector3.MoveTowards (transform.position, currentPoint.Current.position, Time.deltaTime * Speed);

		else if	(Type == FollowType.Lerp)
			transform.position = Vector3.Lerp (transform.position, currentPoint.Current.position, Time.deltaTime * Speed);

		var distanceSquared = (transform.position - currentPoint.Current.position).sqrMagnitude;

		if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
			currentPoint.MoveNext ();
	}



}

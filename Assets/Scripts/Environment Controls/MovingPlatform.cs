﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour 
{
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}

	public FollowType Type = FollowType.MoveTowards;
	public Path Path;
	public float Speed = 1;
	public float MaxDistanceToGoal = 0.1f;

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

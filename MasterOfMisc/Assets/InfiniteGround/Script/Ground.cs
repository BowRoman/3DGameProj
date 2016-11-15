//Copyright Highwalker Studios 2016
//Author: Luc Highwalker
//package: 2D Infinite Ground

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour
{
	[Range(0, 1)] public float modifier;

	Renderer render;

	Vector2 newOffset, offset;
	Vector3 lastPosition;

	void Start()
	{
		render = GetComponent<Renderer>();
	}

	void Update()
	{
		Vector2 oldOffset = offset; //Sets the current texture offset.

		newOffset = (transform.position - lastPosition) * modifier; //Determines where the texture should be offset based on previous position.
		lastPosition = transform.position;

		offset = oldOffset + newOffset; //Sets the new offset.

		render.material.mainTextureOffset = offset; //Applies the new offset.
	}
}
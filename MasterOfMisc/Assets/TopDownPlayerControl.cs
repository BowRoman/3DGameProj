using UnityEngine;
using System.Collections;

public class TopDownPlayerControl : MonoBehaviour {

	[SerializeField]
	float playerSpeedMin = 0.0f; // initial speed of player

	[SerializeField]
	float playerSpeed = 0.0f; // current speed of player

	[SerializeField]
	float playerSpeedMax = 10.0f; // maximum speed of player

	float rightVelocity = 0.0f;
	float upVelocity = 0.0f;
	float downVelocity = 0.0f;
	float leftVelocity = 0.0f;

	[SerializeField]
	float accelerationSpeed = 1.0f;

	float stopped = 0.0f;
 
	void Update()
	{
		MovePlayer();
		ReadDirection();
	}

	void MovePlayer()
	{
		transform.Translate((rightVelocity - leftVelocity) * Time.deltaTime, (upVelocity - downVelocity) * Time.deltaTime, 0);
	}

	void ReadDirection()
	{
		if(downVelocity <= 0)
		{
			upVelocity = setVelocity("w", upVelocity);
			playerSpeed = (rightVelocity + upVelocity + downVelocity + leftVelocity);
		}
		if(rightVelocity <= 0)
		{
			leftVelocity = setVelocity("a", leftVelocity);
			playerSpeed = (rightVelocity + upVelocity + downVelocity + leftVelocity);
		}
		if(upVelocity <= 0)
		{
			downVelocity = setVelocity("s", downVelocity);
			playerSpeed = (rightVelocity + upVelocity + downVelocity + leftVelocity);
		}
		if(leftVelocity <= 0)
		{
			rightVelocity = setVelocity("d", rightVelocity);
			playerSpeed = (rightVelocity + upVelocity + downVelocity + leftVelocity);
		}
	}
	float setVelocity(string key, float velocity)
	{
		if (Input.GetKey(key))
		{
			if (velocity < playerSpeedMin) // give an initial boost
			{
				velocity = playerSpeedMin;
			}
			else if (velocity < playerSpeedMax / 2 || playerSpeed < playerSpeedMax)// if the directional speed limit is not reached or this is the primary direction, increase velocity
			{
				velocity += accelerationSpeed * Time.deltaTime;
			}
			if (velocity > playerSpeedMax)
			{
				velocity = playerSpeedMax;
			}
			// if another direction is being used, adjust so total speed does not go above max.
		}
		else if (velocity > stopped)
		{
			velocity -= accelerationSpeed*2 * Time.deltaTime;
			if (velocity < stopped)
			{
				velocity = stopped;
			}
		}

		return velocity;
	}
}

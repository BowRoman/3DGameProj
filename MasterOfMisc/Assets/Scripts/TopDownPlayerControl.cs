using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopDownPlayerControl : MonoBehaviour {

    public int enemiesToKill = 10; // TEMP! objective is spawned once this reaches 0

    [SerializeField]
    int playerHealthMax = 100;
    int playerHealth;

    [SerializeField]
    Image playerHealthBar;

    [SerializeField]
    float invulnerabilityTime = 0.5f;
    float timeSinceHit;

	// float playerSpeedMin = 0.0f; // initial speed of player
	float playerSpeed = 0.0f; // current speed of player

	[SerializeField]
	float playerSpeedMax = 10.0f; // maximum speed of player
    
    float rightVelocity = 0.0f;
    float upVelocity = 0.0f;
    float downVelocity = 0.0f;
	float leftVelocity = 0.0f;

    bool wPressed = false;
    bool aPressed = false;
    bool sPressed = false;
    bool dPressed = false;
    
	float accelerationSpeed = 1.0f;

	// float stopped = 0.0f;

    [SerializeField]
    Image currentWeaponImage;

    bool collidingWall = false;

    void Start()
    {
        playerHealth = playerHealthMax;
        SetWeapon();
    }
 
	void Update()
    {
        ReadDirection();
        MovePlayer();
        timeSinceHit += Time.deltaTime;
	}

    public void Damage(int points)
    {
        if(timeSinceHit >= invulnerabilityTime)
        {
            timeSinceHit = 0;
            playerHealth -= points;
            playerHealthBar.fillAmount = (float)playerHealth / (float)playerHealthMax;
            if (playerHealth <= 5)
            {
                playerHealth = 0;
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Objective")
        {
            SceneManager.LoadScene(0);
        }
    }

    void SetWeapon()
    {
        currentWeaponImage.sprite = GetComponentInChildren<WeaponControl>().projectile.GetComponent<SpriteRenderer>().sprite;
    }

	void MovePlayer()
	{
        if(!collidingWall)
        {
            float verticalSpeed = upVelocity - downVelocity;
            float horizontalSpeed = rightVelocity - leftVelocity;
            if (verticalSpeed > playerSpeedMax)
            {
                verticalSpeed = playerSpeedMax;
            }
            if (verticalSpeed < 0.0f - playerSpeedMax)
            {
                verticalSpeed = (0.0f - playerSpeedMax);
            }
            if (horizontalSpeed > playerSpeedMax)
            {
                horizontalSpeed = playerSpeedMax;
            }
            if (horizontalSpeed < 0.0f - playerSpeedMax)
            {
                horizontalSpeed = (0.0f - playerSpeedMax);
            }
            transform.Translate((rightVelocity - leftVelocity) * Time.deltaTime, (upVelocity - downVelocity) * Time.deltaTime, 0);
        }
        else
        {
            print("Colliding with Wall");
        }
	}

	void ReadDirection()
	{
		upVelocity = setVelocity("w", upVelocity);
        leftVelocity = setVelocity("a", leftVelocity);
        downVelocity = setVelocity("s", downVelocity);
        rightVelocity = setVelocity("d", rightVelocity);
		playerSpeed = (rightVelocity + upVelocity + downVelocity + leftVelocity);
	}

    float setVelocity(string key, float velocity)
    {
        if (Input.GetKey(key))
        {
            velocity = playerSpeedMax / 2;
            switch (key)
            {
                case "w":
                    wPressed = true;
                    if(leftVelocity+rightVelocity < playerSpeedMax/2)
                    {
                        velocity += (playerSpeedMax / 2) - (leftVelocity + rightVelocity);
                    }
                    if(sPressed)
                    {
                        velocity = 0;
                    }
                    break;
                case "s":
                    sPressed = true;
                    if (leftVelocity + rightVelocity < playerSpeedMax / 2)
                    {
                        velocity += (playerSpeedMax / 2) - (leftVelocity + rightVelocity);
                    }
                    if(wPressed)
                    {
                        velocity = 0;
                    }
                    break;
                case "a":
                    aPressed = true;
                    if (upVelocity + downVelocity < playerSpeedMax / 2)
                    {
                        velocity += (playerSpeedMax / 2) - (upVelocity + downVelocity);
                    }
                    if (dPressed)
                    {
                        velocity = 0;
                    }
                    break;
                case "d":
                    dPressed = true;
                    if (upVelocity + downVelocity < playerSpeedMax / 2)
                    {
                        velocity += (playerSpeedMax / 2) - (upVelocity + downVelocity);
                    }
                    if (aPressed)
                    {
                        velocity = 0;
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            if(velocity > 0)
            {
                velocity -= accelerationSpeed;
            }
            if(velocity < 0)
            {
                velocity = 0;
            }
            switch(key)
            {
                case "w": wPressed = false;
                    break;
                case "a": aPressed = false;
                    break;
                case "s": sPressed = false;
                    break;
                case "d": dPressed = false;
                    break;
                default:
                    break;
            }
        }
        return velocity;
    }
	//float setVelocityAccel(string key, float velocity)
	//{
	//	if (Input.GetKey(key))
	//	{
	//		if (velocity < playerSpeedMin) // give an initial boost
	//		{
	//			velocity = playerSpeedMin;
	//		}
	//		else if (velocity < playerSpeedMax / 2 || playerSpeed < playerSpeedMax)// if the directional speed limit is not reached or this is the primary direction, increase velocity
	//		{
	//			velocity += accelerationSpeed * Time.deltaTime;
	//		}
	//		if (velocity > playerSpeedMax)
	//		{
	//			velocity = playerSpeedMax;
	//		}
	//		// if another direction is being used, adjust so total speed does not go above max.
	//	}
	//	else if (velocity > stopped)
	//	{
	//		velocity -= accelerationSpeed*2 * Time.deltaTime;
	//		if (velocity < stopped)
	//		{
	//			velocity = stopped;
	//		}
	//	}

	//	return velocity;
	//}
}

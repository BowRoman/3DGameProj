using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class GunControl : MonoBehaviour {

	[SerializeField]
	int gunDamage = 50;

	[SerializeField]
	Animator myAnimator;

	[SerializeField]
	AudioSource myAudioSource;

	[SerializeField]
	GameObject PlayerFPSCamera;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			myAnimator.SetTrigger("Shoot");
			myAudioSource.Play();

			RaycastHit hit;
			if(Physics.Raycast(PlayerFPSCamera.transform.position, PlayerFPSCamera.transform.forward, out hit))
			{
				GameObject target = hit.collider.transform.gameObject;
				print(target.name);
				Enemy enemy = target.GetComponent<Enemy>();
				if(target.tag == "Enemy_Head")
				{
					EnemyHead head = target.GetComponent<EnemyHead>();
					enemy = head.GetRootEnemy();
					enemy.Damage(gunDamage*2, PlayerFPSCamera.transform.parent);
				}
                else if(target.tag == "Enemy_Body")
                {
                    EnemyBody body = target.GetComponent<EnemyBody>();
                    enemy = body.GetRootEnemy();
                    enemy.Damage(gunDamage, PlayerFPSCamera.transform.parent);
                }
				else if(enemy)
				{
					enemy.Damage(gunDamage, PlayerFPSCamera.transform.parent);
				}
			}
		}
	}
}

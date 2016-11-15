using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	[SerializeField]
	public int MaxHealth = 100;
	[SerializeField]
	public int CurrentHealth = 100;
	[SerializeField]
	int EnemyDamage = 10;
	[SerializeField]
	float timeToAttack = 0.5f;

	private float attackDuration = 1.5f;
	[SerializeField]
	Image ImgHealth;
	[SerializeField]
	float BaseSpeed = 3.5f;
	[SerializeField]
	Animator myAnimator;
	[SerializeField]
	AudioSource myAudioSource;

	private Transform target;

	void Start()
	{
		attackDuration = timeToAttack;
	}
	void Update()
	{
		if(target)
		{
			GetComponent<NavMeshAgent>().SetDestination(target.position);
			float distance = Vector3.Distance(target.position, transform.position);
			print(distance);
			if(distance > 4.0f)
			{
				myAnimator.SetBool("IsWalking", true);
				timeToAttack = attackDuration;
			}
			else
			{
				myAnimator.SetBool("IsWalking", false);
				Attack();
			}
		}
	}

	public void Damage(int points, Transform newtarget)
	{
		CurrentHealth -= points;
		ImgHealth.fillAmount = (float)CurrentHealth / (float)MaxHealth;
		if (CurrentHealth <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			target = newtarget;
			myAudioSource.Play();
		}
	}

	void Attack()
	{
		timeToAttack -= Time.deltaTime;
		if(timeToAttack > 0)
		{
			return;
		}
		timeToAttack = attackDuration;
		Player player = target.GetComponent<Player>();
		if(player)
		{
			myAnimator.SetTrigger("Attack");
			player.Damage(EnemyDamage,0.5f);
		}
	}
}

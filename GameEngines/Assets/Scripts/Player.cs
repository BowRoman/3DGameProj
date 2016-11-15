using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	[SerializeField]
	public int MaxHealth = 100;
	[SerializeField]
	public int CurrentHealth = 100;
	[SerializeField]
	Image ImgHealth;
	[SerializeField]
	AudioClip PlayerHurt;
	[SerializeField]
	AudioSource myAudioSource;

	public void Damage(int points,float delay = 0.0f)
	{
		CurrentHealth -= points;
		ImgHealth.fillAmount = (float)CurrentHealth / (float)MaxHealth;
		if (CurrentHealth <= 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			myAudioSource.Play();
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyBasicWeaponControl : MonoBehaviour {

    [SerializeField]
    int damageToPlayer = 20;

	void OnTriggerEnter2D(Collider2D victim)
    {
        if (victim.gameObject.tag == "Player")
        {
            victim.gameObject.GetComponent<TopDownPlayerControl>().Damage(damageToPlayer);
        }
    }
}

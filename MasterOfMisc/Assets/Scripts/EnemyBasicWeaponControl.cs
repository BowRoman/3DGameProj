using UnityEngine;
using System.Collections;

public class EnemyBasicWeaponControl : MonoBehaviour {
    
    int damageToPlayer;

    void Start()
    {
        damageToPlayer = transform.parent.gameObject.GetComponent<EnemyBasicControl>().damageToPlayer;
    }

	void OnTriggerEnter2D(Collider2D victim)
    {
        if (victim.gameObject.tag == "Player")
        {
            victim.gameObject.GetComponent<TopDownPlayerControl>().Damage(damageToPlayer);
        }
    }
}

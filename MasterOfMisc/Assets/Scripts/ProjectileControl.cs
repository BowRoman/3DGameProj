using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    [SerializeField]
    float speed = 10.0f;

    [SerializeField]
    int damage = 10;

    float lifespan = 5.0f;
    
	void Update ()
    {
        LifeClock();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.tag == "Player")
        {
            return;
        }
        if (hitObject.tag == "EnemyWeapon")
        {
            hitObject = hitObject.transform.parent.gameObject;
            print("Hit weapon");
        }
        if (hitObject.tag == "Enemy")
        {
            hitObject.GetComponent<EnemyBasicControl>().Damage(damage);
        }
        Destroy(gameObject);
    }
    
    void LifeClock()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

   public float GetSpeed()
    {
        return speed;
    }
}

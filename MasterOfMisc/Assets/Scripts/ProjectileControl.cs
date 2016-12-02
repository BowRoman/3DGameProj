using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    [SerializeField]
    float speed = 10.0f;

    [SerializeField]
    int damage = 10;

    float lifespan = 5.0f;

	void Start ()
    {
        print("Projectile created.");
    }
	
	void Update ()
    {
        LifeClock();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            return;
        }
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.name == "EnemyBasic")
            {
                other.gameObject.GetComponent<EnemyBasicControl>().Damage(damage);
            }
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

using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    [SerializeField]
    public float speed = 10.0f;

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
        Destroy(gameObject);
    }

    // Destroys object when lifespan is exceeded.
    void LifeClock()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}

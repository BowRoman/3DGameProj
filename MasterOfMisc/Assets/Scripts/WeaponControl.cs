using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {

    [SerializeField]
    public GameObject projectile;
    
    Vector3 lookPos;

    void Start()
    {
	
	}
	
	void Update ()
    {
        RotateSpriteToCursor();
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    public void FireBullet()
    {
        GameObject projectileClone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        Vector2 fireDirection = new Vector2(lookPos.x, lookPos.y).normalized;
        float fireSpeed = projectileClone.GetComponent<ProjectileControl>().GetSpeed();
        projectileClone.GetComponent<Rigidbody2D>().velocity = fireDirection * fireSpeed;
    }
    
    void RotateSpriteToCursor()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

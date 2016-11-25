using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {

    [SerializeField]
    GameObject projectile;

    Vector3 lookPos;

    void Start () {
	
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
        GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 1))) as GameObject;
        projectileClone.GetComponent<Rigidbody2D>().velocity = lookPos * projectileClone.GetComponent<ProjectileControl>().speed;
    }

    void RotateSpriteToCursor()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

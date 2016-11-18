using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {
    
	void Start () {
	
	}
	
	void Update ()
    {
        RotateSpriteToCursor();
    }

    void RotateSpriteToCursor()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

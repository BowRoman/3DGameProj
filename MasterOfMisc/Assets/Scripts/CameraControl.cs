using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    GameObject player;

    [SerializeField]
    float cameraSpeed = 5.0f;

    void Start ()
    {
	}
	
	void Update ()
    {
        float zpos = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, cameraSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, zpos);
    }
}

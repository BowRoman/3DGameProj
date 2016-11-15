using UnityEngine;
using System.Collections;

public class EnemyBody : MonoBehaviour
{
    private GameObject rootEnemy;
    void Start()
    {
        GameObject currentParent = transform.parent.gameObject;
        while (currentParent.tag != "Enemy")
        {
            currentParent = currentParent.transform.parent.gameObject;
        }
        rootEnemy = currentParent;
    }

    public Enemy GetRootEnemy()
    {
        return rootEnemy.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
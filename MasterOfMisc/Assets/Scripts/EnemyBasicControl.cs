using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBasicControl : MonoBehaviour {

    [SerializeField]
    int healthMax = 50;
    int health = 50;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    Transform player;
    [SerializeField]
    Animator anim;

    [SerializeField]
    bool touchingPlayer = false;
    [SerializeField]
    bool attacking = false;
    [SerializeField]
    float timeBetweenAttacks = 0.5f;
    float lastAttack = 0.0f;

    bool hasSeenPlayer = false;
    
    void Start()
    {
        Color newAlpha = healthBar.color;
        newAlpha.a = 0.0f;
        healthBar.color = newAlpha;
        health = healthMax;
    }

    void FixedUpdate()
    {
        if(!hasSeenPlayer)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            {
                hasSeenPlayer = true;
            }
        }
        else
        {
            MoveEnemy();
            Attack();
        }
    }

    void Attack()
    {
        lastAttack += Time.deltaTime;
        if (!attacking && touchingPlayer && lastAttack >= timeBetweenAttacks)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                anim.SetTrigger("Attack1");
                attacking = true;
                lastAttack = 0.0f;
            }
        }
    }

    void MoveEnemy()
    {
        float z = Mathf.Atan2((player.position.y - transform.position.y), (player.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        transform.eulerAngles = new Vector3(0, 0, z);

        GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * speed;
    }

    public void Damage(int points)
    {
        if(hasSeenPlayer)
        {
            if (health >= healthMax)
            {
                Color newAlpha = healthBar.color;
                newAlpha.a = 255.0f;
                healthBar.color = newAlpha;
            }
            health -= points;
            healthBar.fillAmount = (float)health / (float)healthMax;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void AnimationComplete()
    {
        attacking = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBasicControl : MonoBehaviour {

    [SerializeField]
    GameObject objective;
    [SerializeField]
    int healthMax = 50;
    int health = 50;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    public int damageToPlayer = 20;
    [SerializeField]
    Transform player;

    [SerializeField]
    Animator anim;
    
    bool touchingPlayer = false;
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
            if(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) // check if this enemy is visible on the screen
            {
                int layermask = 1 << 14; // enemy is on layer 14
                layermask = ~layermask; // inverse layermask so it's all layers except 14
                Vector2 direction = (player.transform.position - transform.position).normalized; // get the direction from this enemy to the player
                RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layermask); // raycast to the player
                if(rayHit.collider.gameObject.tag == "Player") // check if this enemy has a direct line of sight on the player
                {
                    hasSeenPlayer = true;
                }
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
                player.gameObject.GetComponent<TopDownPlayerControl>().enemiesToKill--;
                if(player.gameObject.GetComponent<TopDownPlayerControl>().enemiesToKill <= 0)
                {
                    GameObject objClone = Instantiate(objective, transform.position, new Quaternion()) as GameObject;
                }
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

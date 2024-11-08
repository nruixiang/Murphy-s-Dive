using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State{
        Spawn, Chase, Attack, Death
    }
    public State state;
    public int health;
    public int speed;
    public GameObject player;
    public Transform enemyAttackTransform;
    [SerializeField] GameObject enemyAttackHitbox;
    public float dist;
    public float attackRange;
    public float enemyAttackCooldown;
    public bool canEnemyAttack;
    public Animator anim;
    protected bool isInitialized = false;
    //For LOS
    public LayerMask layerMask;
    public bool InLineOfSight;
    public bool isAttacking = false;
    public SpriteRenderer sr;
    public Color originalColor;
    public AudioSource audioSource;
    [SerializeField] AudioClip enemyDamagedSound;

    //DONT DO ANY AWAKE/START/UPDATE THINGS HERE IT WONT WORK DUMDUM

    protected void ChasePlayer(){
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        if(InLineOfSight){
            if(dist < attackRange && !isAttacking){
            state = State.Attack;
            anim.SetBool("EnemyAttack", true);
            }
        }
        
    }
    protected void AttackPlayer(){
        if(!isAttacking){
            TrackPlayer();
            GameObject attackHitBox = Instantiate(enemyAttackHitbox, enemyAttackTransform.position, enemyAttackTransform.rotation);
            isAttacking = true;

            //This if check is specifically so that the sprite of the melee swing is not upside down. Should not impact the Slime since there is no attack sprite for it, only animation.
            if(player.transform.position.x < this.transform.position.x){
                attackHitBox.transform.localScale = new Vector3(attackHitBox.transform.localScale.x, -attackHitBox.transform.localScale.y, attackHitBox.transform.localScale.z);
            }
            
        }
        CheckPlayerDistance();
        
    }

    protected void TrackPlayer(){
        Transform child = this.gameObject.transform.GetChild(0);
        Vector3 rotation = player.transform.position - transform.position;

        //Bottom 2 Lines is for rotation visualization purposes only, not necessary for final build
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        child.transform.rotation = Quaternion.Euler(0,0,rotZ);

    }
    public void CheckEnemyHealth(){
        if(health <= 0){
            anim.SetTrigger("EnemyDeath");
            GameRoomManager gameRoomManager = FindObjectOfType<GameRoomManager>();
            gameRoomManager.EnemyDefeated();
        } else{
            StartCoroutine(EnemyDamageFeedback());
        }
        
    }

    public void InitializeEnemy(){
        player = GameObject.FindGameObjectWithTag("Player");
        layerMask = ~LayerMask.GetMask("Camera", "Enemy", "Bullet");
        state = State.Spawn;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        dist = Vector2.Distance(transform.position, player.transform.position);
    }
    public IEnumerator SetSlimeStats(){
        yield return new WaitForSeconds(1f);
        anim.SetBool("Spawned", true);
        attackRange = 2f;
        health = 15;
        speed = 2;
        state = State.Chase;
    }
    public IEnumerator SetBeholderStats(){
        yield return new WaitForSeconds(1f);
        anim.SetBool("Spawned", true);
        attackRange = 6f;
        health = 12;
        speed = 1;
        state = State.Chase;
    }
    public IEnumerator SetChampionStats(){
        attackRange = 2f;
        speed = 3;
        yield return new WaitForSeconds(1f);
        anim.SetBool("Spawned", true);
        
        state = State.Chase;
        yield return new WaitForSeconds(0.5f);
    }
    protected void FlipEnemy(){
        if(player.transform.position.x > this.transform.position.x){
            this.GetComponent<SpriteRenderer>().flipX = true;
        } else{
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void LineOfSightCheck(){
        Transform child = this.gameObject.transform.GetChild(0);
        RaycastHit2D hit = Physics2D.Raycast(child.transform.position, player.transform.position - transform.position, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            // Check if the ray hit the player
            if (hit.collider.gameObject.tag == "Player")
            {
                InLineOfSight = true;
            }
            else
            {
                InLineOfSight = false;
            }
        }
        //Optional: Draw a debug line in the editor for visualization
        Debug.DrawLine(child.transform.position, player.transform.position + (player.transform.position - transform.position), Color.red);
    }
    public void ChangeStateIdle(){
        state = State.Spawn;
        
    }
    public void ChangeStateChase(){
        if(isInitialized){
                state = State.Chase;
        }
    }
    public void ChangeStateAttack(){
        state = State.Attack;
    }
    public void ChangeStateDeath(){
        state = State.Death;
    }
    public void ResetAttack(){
        //canEnemyAttack = true;
        isAttacking = false;
    }
    public void CheckPlayerDistance(){
        if(dist > attackRange){
        state = State.Chase;
        anim.SetBool("EnemyAttack", false);
        isAttacking = false;
        }
    }
    public void DestroyEnemy(){ 
        Destroy(gameObject);
    }
    public IEnumerator EnemyDamageFeedback(){
        if (enemyDamagedSound != null){
        audioSource.PlayOneShot(enemyDamagedSound);
    } else {
        Debug.LogWarning("enemyDamagedSound is not assigned for this enemy!");
    }
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalColor;
    }
}

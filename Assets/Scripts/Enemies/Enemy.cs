using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State{
        Spawn, Chase, Attack,
    }
    public State state;
    public int health;
    public int speed;
    public GameObject player;
    [SerializeField] Transform enemyAttackTransform;
    [SerializeField] GameObject enemyAttackHitbox;
    public float dist;
    public float attackRange;
    public float enemyAttackCooldown;
    public bool canEnemyAttack;
    public Animator anim;

    //DONT DO ANY AWAKE/START/UPDATE THINGS HERE IT WONT WORK DUMDUM

    protected void ChasePlayer(){
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        if(dist < attackRange){
            state = State.Attack;
        }
    }
    protected void AttackPlayer(){
        if(canEnemyAttack){
            StartCoroutine(AttackCooldown());
            //Debug.Log("Attacking Player");
            TrackPlayer();
            Instantiate(enemyAttackHitbox, enemyAttackTransform.position, enemyAttackTransform.rotation);
            canEnemyAttack = false;
        }
        if(dist > attackRange){
            state = State.Chase;
        }
    }
    protected void TrackPlayer(){
        Transform child = this.gameObject.transform.GetChild(0);
        Vector3 rotation = player.transform.position - transform.position;

        //Bottom 2 Lines is for rotation visualization purposes only, not necessary for final build
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        child.transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
    IEnumerator AttackCooldown(){
        //Debug.Log("CD CALLED");
        yield return new WaitForSeconds(enemyAttackCooldown);
        canEnemyAttack = true;
    }
    public void CheckEnemyHealth(){
        if(health <= 0){
            Destroy(gameObject);
            GameRoomManager gameRoomManager = FindObjectOfType<GameRoomManager>();
            gameRoomManager.EnemyDefeated();
        }
        
    }
    public void InitializeEnemy(){
        player = GameObject.FindGameObjectWithTag("Player");
        state = State.Spawn;
        anim = GetComponent<Animator>();
        canEnemyAttack = false;
        dist = Vector2.Distance(transform.position, player.transform.position);
    }
    public IEnumerator SetSlimeStats(){
        yield return new WaitForSeconds(1f);
        anim.SetBool("Spawned", true);
        attackRange = 2f;
        enemyAttackCooldown = 2f;
        health = 10;
        speed = 1;
        canEnemyAttack = true;
        state = State.Chase;
    }
    public IEnumerator SetBeholderStats(){
        attackRange = 6f;
        enemyAttackCooldown = 2f;
        health = 10;
        speed = 1;
        yield return new WaitForSeconds(1f);
        anim.SetBool("Spawned", true);
        
        state = State.Chase;
        yield return new WaitForSeconds(0.5f);
        canEnemyAttack = true;
    }
        protected void FlipEnemy(){
        if(player.transform.position.x > this.transform.position.x){
            this.GetComponent<SpriteRenderer>().flipX = true;
        } else{
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}

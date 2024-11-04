using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public enum AttackType{
        Melee, Ranged
    }
    private AttackType currentAttackType;
    [SerializeField] float attackSwitchCooldown;
    [SerializeField] GameObject bossRangedAttack;
    private float attackSwitchTimer;
    // Start is called before the first frame update
    void Start()
    {
        attackSwitchCooldown = 5f;
        attackSwitchTimer = attackSwitchCooldown;
        InitializeEnemy();
        currentAttackType = AttackType.Melee;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Spawn:
            if(!isInitialized){
                StartCoroutine(SetChampionStats());
                isInitialized = true;
            }
            break;
            case State.Chase:
            ChasePlayer();
            break;
            case State.Attack:
            //AttackPlayer();
            break;
            case State.Death:
            //just die bro.
            break;
        }
        dist = Vector2.Distance(transform.position, player.transform.position); //Continuosly update it to transition back and forth between States
        FlipEnemy();
        Debug.Log(state);
        Debug.Log(currentAttackType);
        LineOfSightCheck();

        if(attackSwitchTimer > 0){
            attackSwitchTimer -= Time.deltaTime;
        } else{
            CycleAttacks();
            attackSwitchTimer = attackSwitchCooldown;
        }
    }
    private void CycleAttacks(){
        if(currentAttackType == AttackType.Melee){
            currentAttackType = AttackType.Ranged;
            attackRange = 6f;
        } else if(currentAttackType == AttackType.Ranged){
            currentAttackType = AttackType.Melee;
            attackRange = 3f;
        }
    }
    //To use in animator to attack player
    protected void BossAttackPlayer(){
        if(currentAttackType == AttackType.Melee){
            AttackPlayer();
        } else if(currentAttackType == AttackType.Ranged){
            BossRangedAttackPlayer();
        }
    }
    protected void BossRangedAttackPlayer(){
        if(!isAttacking){
            TrackPlayer();
            Instantiate(bossRangedAttack, enemyAttackTransform.position, enemyAttackTransform.rotation);
            isAttacking = true;
            
        }
        CheckPlayerDistance();
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    // Start is called before the first frame update
    void Awake(){
        state = State.Chase;
        canEnemyAttack = true;
        dist = Vector2.Distance(transform.position, player.transform.position); //Initialize dist at start to prevent from going into Attack State at the start
    }
    void Start()
    {
        //Variables to initialize using a database (JSON/CSV)
        attackRange = 2f;
        enemyAttackCooldown = 2f;
        health = 10;
        speed = 1;

    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Chase:
            ChasePlayer();
            break;
            case State.Attack:
            AttackPlayer();
            break;
        }
        dist = Vector2.Distance(transform.position, player.transform.position); //Continuosly update it to transition back and forth between States
    }
}

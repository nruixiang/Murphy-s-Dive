using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    
    // Start is called before the first frame update
    void Awake(){
        InitializeEnemy();
    }
    void Start()
    {
        
        
        //Variables to initialize using a database (JSON/CSV)

    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Spawn:
            if(!isInitialized){
                StartCoroutine(SetSlimeStats());
                isInitialized = true;
            }
            break;
            case State.Chase:
            ChasePlayer();
            break;
            case State.Attack:
            //AttackPlayer();
            break;
        }
        dist = Vector2.Distance(transform.position, player.transform.position); //Continuosly update it to transition back and forth between States
        FlipEnemy();
        //Debug.Log(state);
        LineOfSightCheck();
        
    }
    
    
}

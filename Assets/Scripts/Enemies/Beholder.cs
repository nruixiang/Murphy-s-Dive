using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : Enemy
{
    [SerializeField] AudioClip slimeAttack;
    void Awake(){
        InitializeEnemy();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Spawn:
            if(!isInitialized){
                StartCoroutine(SetBeholderStats());
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
        //Debug.Log(state);
        LineOfSightCheck();
    }
    public void PlayAttackSound(){
        audioSource.PlayOneShot(slimeAttack);
    }
}

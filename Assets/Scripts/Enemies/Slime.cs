using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Slime : Enemy
{
    [SerializeField] AudioClip[] slimeSteps;
    [SerializeField] AudioClip slimeAttack;
    // Start is called before the first frame update
    void Awake(){
        InitializeEnemy();
        audioSource = GetComponent<AudioSource>();
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
            case State.Death:
            //just die bro.
            break;
        }
        dist = Vector2.Distance(transform.position, player.transform.position); //Continuosly update it to transition back and forth between States
        FlipEnemy();
        //Debug.Log(state);
        LineOfSightCheck();
        
    }
    
    public void PlayRandomFootstep(){
        if(slimeSteps.Length == 0) return;

        int randomIndex = Random.Range(0, slimeSteps.Length);
        AudioClip clip = slimeSteps[randomIndex];
        audioSource.PlayOneShot(clip);
    }
    public void PlayAttackSound(){
        audioSource.PlayOneShot(slimeAttack);
    }
}

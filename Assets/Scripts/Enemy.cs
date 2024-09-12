using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State{
        Chase, Attack,
    }
    private State state;
    public int health = 10;
    public int speed = 1;
    public GameObject player;
    private float dist;

    void Awake(){
        state = State.Chase;
    }
    void Update(){
        ChasePlayer();
        switch(state){
            default:
                case State.Chase:
                break;
                case State.Attack:
                break;
        }
    }
    private void ChasePlayer(){
        dist = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void AttackPlayer(){

    }
}

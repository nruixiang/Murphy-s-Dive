using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float maxHealth;
    [SerializeField] float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);

        if(horizontal >= 1){
            sr.flipX = true;
        } else if(horizontal < 0){
            sr.flipX = false;
        } 
    }
    public void PlayerTakeDamage(){
        HealthManager.health--;
        StartCoroutine(DamageFeedback());
        if(HealthManager.health == 0){
            //Die
        }
    }
    public IEnumerator DamageFeedback(){
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

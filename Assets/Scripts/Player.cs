using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float maxHealth;
    private Animator anim;
    [SerializeField] float movementSpeed;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        
        if(vertical == 0){
            anim.SetBool("Moving", false);
        } else{
            anim.SetBool("Moving", true);
        }
        if(horizontal >= 1){
            sr.flipX = true;

            anim.SetBool("Moving", true);

        } else if(horizontal < 0){
            sr.flipX = false;

            anim.SetBool("Moving", true);
        }
    }
    public void PlayerTakeDamage(){
        HealthManager.health--;
        StartCoroutine(DamageFeedback());
        if(HealthManager.health == 0){
            uiManager.GameOver();
        }
    }
    public IEnumerator DamageFeedback(){
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

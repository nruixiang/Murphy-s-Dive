using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //public float maxHealth;
    private Animator anim;
    [SerializeField] float movementSpeed;
    private UIManager uiManager;
    private AudioSource audioSource;
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] AudioClip playerDamagedSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(playerDamagedSound);
        StartCoroutine(DamageFeedback());
        if(HealthManager.health == 0){
            anim.SetTrigger("PlayerDeath");
        }
    }
    public IEnumerator DamageFeedback(){
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void PlayerIsDead(){
        uiManager.GameOver();
    }
    public void PlayRandomFootstep(){
        if(footstepClips.Length == 0) return;

        int randomIndex = Random.Range(0, footstepClips.Length);
        AudioClip clip = footstepClips[randomIndex];
        audioSource.PlayOneShot(clip);
    }
}

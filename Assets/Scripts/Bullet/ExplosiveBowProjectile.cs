using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBowProjectile : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private float initialForce = 28f;    // Initial force when shot
    private float force;                // Force used for both deceleration and returning
    private float maxForce = 28f;       // Max speed when returning to the player
    private float decelerationRate = 12f; // Speed reduction rate
    private float accelerationRate = 12f; // Speed increase rate for returning to the player
    private float minSpeedThreshold = 0.5f; // Speed at which projectile starts returning
    private bool decelerating = false;   // True while decelerating
    private bool returning = false;     // True when returning to player
    [SerializeField] Transform target;
    [SerializeField] int damage;
    Vector3 targetPos;

    void Start()
    {
        // Find the player
        target = GameObject.FindWithTag("Player").transform;
        
        // Get the main camera and mouse position
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Set initial shot force
        force = initialForce;

        // Shoot the projectile in the direction of the mouse
        Vector2 direction = (mousePos - transform.position).normalized;
        rb.velocity = direction * force;

        // Rotate the projectile to face the direction of shooting
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        StartCoroutine(Deceleratenow());
    }

    void Update()
    {
        // Decelerate the projectile first
        if (decelerating)
        {
            // Reduce the current velocity using the deceleration rate
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, decelerationRate * Time.deltaTime);

            // When the velocity falls below the threshold, stop decelerating
            if (rb.velocity.magnitude < minSpeedThreshold)
            {
                decelerating = false;
                returning = true;
                force = 0; // Reset the force to start slow when returning
            }
        }

        // Once decelerating stops, initiate return phase
        if (returning)
        {
            targetPos = target.position;

            // Gradually increase the force/speed as the projectile flies back
            if (force < maxForce)
            {
                force += accelerationRate * Time.deltaTime;
            }

            // Continuously calculate direction back to the player
            Vector2 returnDirection = (targetPos - transform.position).normalized;

            // Update the Rigidbody2D velocity to move back to the player
            rb.velocity = returnDirection * force;

            // Rotate the projectile to face the player as it moves
            float angle = Mathf.Atan2(returnDirection.y, returnDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy"){
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
            enemy.CheckEnemyHealth();
            Destroy(gameObject);
        } else if(col.gameObject.tag == "Player"){
            Player player = col.gameObject.GetComponent<Player>();
            player.PlayerTakeDamage();
            Destroy(gameObject);
        }
    
        
    }
    IEnumerator Deceleratenow(){
        yield return new WaitForSeconds(0.5f);
        decelerating = true;
    }

}
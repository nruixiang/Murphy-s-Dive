using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();         
        Vector2 direction = transform.right;
        rb.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy"){
            Destroy(gameObject);

        } else if(col.gameObject.tag == "Player"){
            Player player = col.gameObject.GetComponent<Player>();
            player.PlayerTakeDamage();
            Destroy(gameObject);
        }
        
    }
}

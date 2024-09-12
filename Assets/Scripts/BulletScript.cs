using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector2 dir;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction =  mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        dir = new Vector2(direction.x, direction.y).normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy"){
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
            Destroy(gameObject);
        } else{
            var firstContact = col.contacts[0];
            Vector2 newVelocity = Vector2.Reflect(dir.normalized, firstContact.normal);
            rb.velocity = newVelocity * force;
            dir = newVelocity;
            Debug.Log("Collided");
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltBulletScript : MonoBehaviour
{
    [SerializeField] int damage;
    private Vector3 mousePos;
    private Vector2 dir;
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction =  mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        dir = new Vector2(direction.x, direction.y).normalized;
        StartCoroutine(BulletFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Enemy"){
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
        }
    }
    public IEnumerator BulletFade(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

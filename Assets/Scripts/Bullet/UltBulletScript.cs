using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltBulletScript : MonoBehaviour
{
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
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
            enemy.CheckEnemyHealth();
            Debug.Log("IT HIIIIIT");
        }
    }
    public IEnumerator BulletFade(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

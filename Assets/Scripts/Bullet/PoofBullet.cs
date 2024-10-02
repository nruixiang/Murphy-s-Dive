using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofBullet : MonoBehaviour
{
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
        if(col.gameObject.tag == "Player"){
            Player player = col.gameObject.GetComponent<Player>();
            player.PlayerTakeDamage();
            Destroy(gameObject);
        }
    }
    public IEnumerator BulletFade(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

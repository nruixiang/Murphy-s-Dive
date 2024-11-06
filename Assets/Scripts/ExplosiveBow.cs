using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBow : Weapon
{
    [SerializeField] GameObject img1;
    [SerializeField] GameObject img2;
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] GameObject projectile;
    public Transform projectileTransform;
    public bool canFire;
    private float timer = 1;
    [SerializeField] int ultDamage;
    [SerializeField] Animator anim;
    
    private void OnEnable(){
        img1.SetActive(false);
        img2.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        //Bottom 2 Lines is for rotation visualization purposes only, not necessary for final build
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Shoot();
                
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            UltShoot();
        }
        if(canFire == false){
                timer -= Time.deltaTime;
                if(timer <= 0){
                    canFire = true;
                    timer = 1;
                }
        }
        
        
    }
    void Shoot(){
        if(canFire == true){
            Instantiate(projectile, projectileTransform.position, Quaternion.identity);
            canFire = false;
            } 
    }
    void UltShoot(){
        if(UIManager.ultReady == true){
            anim.SetTrigger("UltFired");
            UIManager.ultReady = false;
            UIManager.ultCharge = 0;
            StartCoroutine(UltDamage());
            
        
        }
    }
    private IEnumerator UltDamage(){
        yield return new WaitForSeconds(1.7f);
        Debug.Log("Ult Damage Hit");
        GameRoomManager gameRoomManager = FindObjectOfType<GameRoomManager>();
            List<Enemy> enemies = gameRoomManager.GetEnemiesInCurrentRoom();

            foreach (Enemy enemy in enemies)
            {
                enemy.health -= ultDamage;
                enemy.CheckEnemyHealth();
            }

            // Reset or deactivate the ultimate ability
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RangedWeapon : Weapon
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject ultProjectile;
    public Transform projectileTransform;
    public bool canFire;
    private float timer = 1;
    
    
    // Start is called before the first frame update
    void Awake(){
        Weapon_ID = 1001;
        //SetWeaponStats();

    }
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
            Instantiate(ultProjectile, projectileTransform.position, projectileTransform.rotation);
            UIManager.ultReady = false;
            UIManager.ultCharge = 0;
        }
    }
}

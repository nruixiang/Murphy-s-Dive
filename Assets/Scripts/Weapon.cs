using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CSVReader csvReader;
    public int Weapon_ID;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWeaponStats(){
        WeaponStats weaponStats = csvReader.GetWeaponById(Weapon_ID);
        damage = weaponStats.damage;

    }
}

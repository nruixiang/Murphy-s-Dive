using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int Weapon_ID;
    public float damage;

    public WeaponStats(int Weapon_ID, float damage){
        this.Weapon_ID = Weapon_ID;
        this.damage = damage;
    }
    
}

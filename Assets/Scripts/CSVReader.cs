using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public TextAsset weaponCSV;
    private List<WeaponStats> weaponStatsList;

    void Awake()
    {
        weaponStatsList = ReadWeaponCSV(weaponCSV);
    }
    public static List<WeaponStats> ReadWeaponCSV(TextAsset csv)
    {
        List<WeaponStats> weaponStatsList = new List<WeaponStats>();
        StringReader reader = new StringReader(csv.text);

        //Skip the header line
        reader.ReadLine();

        //Read each line
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] values = line.Split(',');

            if (values.Length == 2) //Adjusted length check to 3
            {
                int Weapon_ID = int.Parse(values[0]);
                float damage = float.Parse(values[1]);


                WeaponStats stats = new WeaponStats(Weapon_ID, damage);
                weaponStatsList.Add(stats);
            }
        }
        return weaponStatsList;
    }
    public string GetWeaponStatsById(int id)
    {
        WeaponStats weaponStats = weaponStatsList.Find(stat => stat.Weapon_ID == id);
        if (weaponStats != null)
        {
            return $"ID: {weaponStats.Weapon_ID}, DAMAGE: {weaponStats.damage}";
        }
        else
        {
            return $"No stats found for ID: {id}";
        }
    }
    public WeaponStats GetWeaponById(int id)
    {
        return ReadWeaponCSV(weaponCSV).Find(weaponStats => weaponStats.Weapon_ID == id);
    }
}

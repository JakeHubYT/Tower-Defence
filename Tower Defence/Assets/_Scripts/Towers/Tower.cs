using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public new string name;
    public GameObject model;
    public float damage;
    public float fireRate;
    public float sightRadius;

    public int price;
    
   

}

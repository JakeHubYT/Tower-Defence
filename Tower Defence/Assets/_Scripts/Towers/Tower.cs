using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public new string name;
    public GameObject model;
    public GameObject projectilePrefab;
    public bool oneShotAudio = true;
    public AudioClip fireSound;

    public float damage;
    public float fireRate;
    public float sightRadius;

    public int price;
    
   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public GameObject model;
    public float health = 100;
    public float damage;
    public float speed;
    public AudioClip dieSound;


    public float cameraShakeStrength = .025f;


    public int moneyOnDeath = 2;


}


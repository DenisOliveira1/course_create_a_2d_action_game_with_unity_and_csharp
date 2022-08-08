using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // *******************************************************
    public int health;
    public int damage;

    public float speed;
    public float timeBetweenAttacks;

    public int pickupChance;
    public GameObject[] pickups;

    public int healthPickupChance;
    public GameObject[] healthPickups;

    public GameObject deathStain;
    public GameObject deathEffect;
    
    // *******************************************************

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    [HideInInspector]
    public Transform player; 

    public virtual void TakeDamage(int damage){
        health -= damage;
        if (IsDead())
        {
            Instantiate(deathStain, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            GenerateDrop();
            Destroy(gameObject);
        }
    }

    private void GenerateDrop()
    {
        GenerateWeaponDrop();
        GenerateHealthDrop();
    }

    private void GenerateWeaponDrop()
    {
        int randomNumber = GetRandomNumber();
        if (randomNumber < pickupChance)
        {
            GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
            Instantiate(randomPickup, transform.position, transform.rotation);
        }
    }

    private void GenerateHealthDrop()
    {
        int randomNumber =  GetRandomNumber();
        if (randomNumber < healthPickupChance)
        {
            GameObject randomPickup = healthPickups[Random.Range(0, healthPickups.Length)];
            Instantiate(randomPickup, transform.position, transform.rotation);
        }
    }

    private int GetRandomNumber(){
        return Random.Range(0, 101);
    }

    private bool IsDead(){
        return health <= 0;
    }
}


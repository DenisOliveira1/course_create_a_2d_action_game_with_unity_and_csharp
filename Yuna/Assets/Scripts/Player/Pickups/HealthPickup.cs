using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public Player playerScript;
    public int healAmount;

    public GameObject effect;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player"){
            Instantiate(effect, transform.position, transform.rotation);
            playerScript.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public int damage;
    public int speed;
    public GameObject explosion;

    private Player playerScript;
    private Vector2 targetPosition;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    void Update()
    {
        if (IsBulletNotInTarget())
            MoveProjectile();
        else
            DestroyProjectile();
    }

    private void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private bool IsBulletNotInTarget(){
        return Vector2.Distance(transform.position, targetPosition) > .1f;
    }

    private void OnTriggerEnter2D(Collider2D collision) {   
        if(collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile(){
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

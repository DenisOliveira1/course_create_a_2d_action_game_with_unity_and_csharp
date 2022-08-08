using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    public int damage;
    public bool isDestroyedOnHit;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void DestroyProjectile(){
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {   
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            if (isDestroyedOnHit) DestroyProjectile();
        }
        if(collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            if (isDestroyedOnHit) DestroyProjectile();
        }
    }
}

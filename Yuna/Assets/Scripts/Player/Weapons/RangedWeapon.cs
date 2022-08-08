using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private Animator animatorCamera;

    private float shotTime;

    void Start(){
        animatorCamera = Camera.main.GetComponent<Animator>();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if(Input.GetMouseButton(0)){
            if(Time.time >= shotTime)
            {
                animatorCamera.SetTrigger("shake");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}

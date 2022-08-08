using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    // *******************************************************
    public float stopDistance;
    // *******************************************************

    private float attackTime;
    private Animator animator;
    public Transform shotPoint;
    public GameObject enemyProjectile;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayerAlive())
            if (!IsEnemyCloseEnough())
                FollowPlayer();
            if(IsTimeToAttack() && IsEnemyCloseEnough())
                StartAttaking();
    }

    public void RangedAttack(){
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(enemyProjectile, shotPoint.position, shotPoint.rotation);
    }

    private bool IsPlayerAlive()
    {
        return player != null;
    }

    private bool IsEnemyCloseEnough()
    {
        return Vector2.Distance(transform.position, player.position) <= stopDistance;
    }

    private void FollowPlayer(){
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private bool IsTimeToAttack()
    {
        return Time.time >= attackTime;
    }

    private void StartAttaking()
    {
        attackTime = Time.time + timeBetweenAttacks;
        animator.SetTrigger("attack");
    }
}

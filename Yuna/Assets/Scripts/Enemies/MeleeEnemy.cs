using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // *******************************************************
    public float stopDistance;
    public float attackSpeed;
    // *******************************************************

    private float attackTime;

    public override void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        if (IsPlayerAlive())
            if (IsEnemyCloseEnough())
                FollowPlayer();
            else
                if (IsTimeToAttack())
                    AttackPlayer();
    }

    private bool IsEnemyCloseEnough()
    {
        return Vector2.Distance(transform.position, player.position) > stopDistance;
    }

    private bool IsPlayerAlive()
    {
        return player != null;
    }

    private void FollowPlayer(){
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        StartCoroutine(MeeleAttack());
        attackTime = Time.time + timeBetweenAttacks;
    }

    private bool IsTimeToAttack()
    {
        return Time.time >= attackTime;
    }

    IEnumerator MeeleAttack(){
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        
        float percent = 0;
        while(percent <= 1){
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent,2)+ percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}

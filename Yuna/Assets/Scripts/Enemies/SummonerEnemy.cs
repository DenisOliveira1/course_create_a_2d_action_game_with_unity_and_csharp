using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{
    // *******************************************************
    public float stopDistance;

    // *******************************************************

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;
    private Animator animator;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy ememyToSummon;

    public GameObject summonEffect;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        StartCoroutine(SetNewTargetPosition());
    }

    void Update()
    {
        if(IsPlayerAlive())
        {
            if (!HasSummonerReachedItsTarget())
                RunToTarget();
            else
            {
                animator.SetBool("isRunning", false);
                if (IsTimeToSummon())
                    StartSummoning();
            }
            if(IsPlayerCloseEnough()){
                StartCoroutine(SetNewTargetPosition());
                if (!HasSummonerReachedItsTarget())
                    RunToTarget();
            }
        }
    }

    public void Summon(){
        if (IsPlayerAlive()){
            Instantiate(summonEffect, transform.position, transform.rotation);
            Instantiate(ememyToSummon, transform.position, transform.rotation);
        }
    }

    private void StartSummoning()
    {
        summonTime = Time.time + timeBetweenSummons;
        animator.SetTrigger("summon");
    }

    private bool IsTimeToSummon()
    {
        return Time.time >= summonTime;
    }

    private void RunToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        animator.SetBool("isRunning", true);
    }

    private void FollowPlayer(){
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private bool HasSummonerReachedItsTarget()
    {
        return Vector2.Distance(transform.position, targetPosition) <= .5f;
    }

    private bool IsPlayerAlive()
    {
        return player != null;
    }

    private bool IsPlayerCloseEnough()
    {
        return Vector2.Distance(transform.position, player.position) <= stopDistance;
    }

    IEnumerator SetNewTargetPosition(){
        
        if (player.position.x < 0) minX = player.position.x + stopDistance;
        if (player.position.x >= 0) minX = player.position.x - stopDistance;

        if (player.position.y < 0) minY = player.position.y + stopDistance;
        if (player.position.y >= 0) minY = player.position.y - stopDistance;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPosition = new Vector2(randomX, randomY);

        yield return null;
    }
}

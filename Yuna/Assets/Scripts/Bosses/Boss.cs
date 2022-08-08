using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    private float attackTime;

    public Enemy[] enemies;
    public float spawnOffset;

    private int maxHealth;
    private int halfHealth;
    private Animator animator;

    public GameObject summonEffect;

    private Slider healthBarSlider;

    private bool isBossSupposedToAttack = false;

    public GameObject attackEffect;
    private SceneTransitions sceneTransitions;

    public override void Start()
    {
        base.Start();
        maxHealth = health;
        halfHealth = maxHealth/2;
        animator = GetComponent<Animator>();

        healthBarSlider = FindObjectOfType<Slider>();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;

        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBarSlider.value = health;

        SummonRandomEnemy();

        if(IsBossWithHalfHealth()){
            isBossSupposedToAttack = true;
            animator.SetTrigger("stage2");
        }

        if(IsBossDead())
        {
            healthBarSlider.gameObject.SetActive(false);
            sceneTransitions.LoadScene("MenuWinScene");
        }
    }

    private bool IsBossDead(){
        return health <= 0; 
    }

    private void SummonRandomEnemy()
    {
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(summonEffect, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private bool IsBossWithHalfHealth(){
        return health < halfHealth;
    }

    private void OnTriggerStay2D(Collider2D collision){ 
        if (collision.tag == "Player" && IsTimeToAttack() && isBossSupposedToAttack){
            AttackPlayer(collision);
        }
    }

    private void AttackPlayer(Collider2D collision)
    {
        Instantiate(attackEffect, transform.position, transform.rotation);
        collision.GetComponent<Player>().TakeDamage(damage);
        attackTime = Time.time + timeBetweenAttacks;
    }

    private bool IsTimeToAttack()
    {
        return Time.time >= attackTime;
    }
}
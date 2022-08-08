using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ************************ ficha ************************
    private int level;
    
    public int health;
    public float speed;
    // *******************************************************
    
    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private Animator animator;
    private Animator animatorCamera;
    public Animator animatorPanel;

    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartEmpty;
    private int maxHealth;

    public GameObject deathStain;
    public GameObject deathEffect;

    private SceneTransitions sceneTransitions;

    void Start()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animatorCamera = Camera.main.GetComponent<Animator>();
    
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    void Update()
    {
        var moveInput = GetMoveInput();
        SetIsRunning(moveInput);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    private Vector2 GetMoveInput()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(horizontalInput, verticalInput);
        moveAmount = moveInput.normalized * speed;
        return moveInput;
    }

    private void SetIsRunning(Vector2 moveInput)
    {
        if(moveInput != Vector2.zero) animator.SetBool("isRunning", true);
            else animator.SetBool("isRunning", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animatorCamera.SetTrigger("shake");
        animatorPanel.SetTrigger("hurt");
        UpdateHeathUI();
        if (IsDead()){
            Instantiate(deathStain, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            sceneTransitions.LoadScene("MenuLoseScene");
        }
    }

    private bool IsDead()
    {
        return health <= 0;
    }

    public void ChangeWeapon(RangedWeapon weaponToEquip){
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    public void Heal(int healAmount){
        if (health + healAmount > maxHealth)
            health = maxHealth;
        else
            health += healAmount;
        UpdateHeathUI();
    }

    private void UpdateHeathUI(){
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i< health)
                hearts[i].sprite = heartFull;
            else
                hearts[i].sprite = heartEmpty;
        }
    }
}
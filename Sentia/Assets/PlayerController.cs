//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCtrl : MonoBehaviour
{

    public float moveSpeed;
    public Weapon weapon;
    Rigidbody2D rb; 
    InputAction moveAction;
    InputAction attackAction;
    public GameObject firePoint;
    public int gameScore;

    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject canvas;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        //handle movement
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = moveValue * moveSpeed; 

        if (moveValue.x > 0) {spriteRenderer.flipX = false;}
        if (moveValue.x < 0) {spriteRenderer.flipX = true;}
        
        if (moveValue.magnitude > 0) 
        {
            animator.SetBool("BisMoving", true);
        } else {
            animator.SetBool("BisMoving", false);
        }


        //handle attack direction
        CheckAttack();
        
    }

    void CheckAttack()
    {
        Vector2 attackDirection = attackAction.ReadValue<Vector2>();
        firePoint.transform.localPosition = new Vector3(0.175f * attackDirection.x, 0.175f * attackDirection.y, 0);
        
        firePoint.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(attackDirection.y, attackDirection.x)* Mathf.Rad2Deg-90);
        if (attackDirection.magnitude > 0){weapon.Fire();}


        //handles sprite direction
        if (attackDirection.x > 0) {spriteRenderer.flipX = false;}
        if (attackDirection.x < 0) {spriteRenderer.flipX = true;}
    }

    public void IncreaseScore()
    {
        gameScore++;
        canvas.GetComponent<HealthUIScript>().updateScoreUI();
    }
}


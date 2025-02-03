using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{

    public float moveSpeed;
    public Weapon weapon;
    Rigidbody2D rb; 
    InputAction moveAction;
    InputAction attackAction;
    public GameObject firePoint;

    public Animator animator;
    private SpriteRenderer spriteRenderer;

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
        UnityEngine.Vector2 moveValue = moveAction.ReadValue<UnityEngine.Vector2>();
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
        UnityEngine.Vector2 attackDirection = attackAction.ReadValue<UnityEngine.Vector2>();
        firePoint.transform.localPosition = new UnityEngine.Vector3(0.175f * attackDirection.x, 0.175f * attackDirection.y, 0);
        if (attackDirection.magnitude > 0){weapon.Fire();}
    }
}


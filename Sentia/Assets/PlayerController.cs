//using System.Numerics;
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
    InputAction pauseAction;
    public GameObject firePoint;
    public int gameScore;

    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject canvas;

    public GameObject enemySpawner;

    Vector2 mouseDirection;
    Vector2 mousePosition;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        pauseAction = InputSystem.actions.FindAction("Pause");
        
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    Vector2 ClampDistance(Vector2 targetPos, Vector2 objectPos, Vector2 centerPos, float maxDistance)
    {
        // Move towards the target
        Vector2 desiredPosition = targetPos;

        // Calculate the distance from the center object
        float distanceFromCenter = Vector2.Distance(centerPos, desiredPosition);

        // If the new position exceeds the max allowed distance, clamp it
        if (distanceFromCenter > maxDistance)
        {
            Vector2 direction = (desiredPosition - centerPos).normalized;
            return centerPos + direction * maxDistance; // Clamp to radius
        }

        return desiredPosition; // Return the original target position if within bounds
    }


    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.rotation = Quaternion.Euler(0,0,aimAngle);

        Vector2 playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 firepointPos = new Vector2(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y);
        
        firepointPos = ClampDistance(mousePosition, firepointPos, playerPos, 0.175f);

        firePoint.transform.position = new Vector3(firepointPos.x, firepointPos.y, 0);
    }

    void Update()
    {

        //handle mouse
        if (Input.GetMouseButton(0)){
            weapon.Fire();
            
        }
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

        //handle pause
        if (pauseAction.WasPressedThisFrame()){
            if (canvas.GetComponent<pauseMenuScript>().paused)
            {
                canvas.GetComponent<pauseMenuScript>().UnPause();
            } else 
            {
                canvas.GetComponent<pauseMenuScript>().Pause();
            }
            
        }

        //handle attack direction
        //CheckAttack();
        
    }

    //old code for arrow key aiming and shooting
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

    public void IncreaseScore(int num)
    {
        gameScore += num;
        canvas.GetComponent<HealthUIScript>().updateScoreUI();

        if (gameScore % 50 == 0) {
            enemySpawner.GetComponent<enemySpawner>().SpawnBoss();
        }
    }

    public void SaveHighScore()
{
    int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
    if (gameScore > currentHighScore)
    {
        PlayerPrefs.SetInt("HighScore", gameScore);
        PlayerPrefs.Save();
    }
}
}


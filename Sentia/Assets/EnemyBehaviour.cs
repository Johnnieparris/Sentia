// using Unity.VisualScripting;
// using UnityEngine;

// public class EnemyBehaviour : MonoBehaviour
// {
//     private Transform playerTransform;
//     public float speed;
//     public float gridSize = 0.5f;

//     private Rigidbody2D rb;
//     private bool isMoving = false;
//     private Vector3 targetPosition;

//     public Animator animator;

//     public GameObject heart;
//     public GameObject milk;
//     public int heartDropChance = 20;
//     public int milkDropChance = 20;

//     public int pointsWorth = 1;

//     public GameObject acid;
    

//     private Vector2 targetDirection;
//     public GameObject audioHandler;

//     private void Awake()
//     {
//         playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
//         animator = GetComponent<Animator>();
//         rb = GetComponent<Rigidbody2D>();
//         audioHandler = GameObject.FindGameObjectWithTag("Audio");

//     }

//     void Update()
//     {
//         if (!isMoving)
//         {
//             // Calculate the direction to move
//             Vector3 direction = CalculateDirection();

//             // Set the target position based on the direction and grid size
//             targetPosition = transform.position + direction * gridSize;

//             // Start moving toward the target position
//             StartCoroutine(MoveToTarget(targetPosition));
//         }
//     }

//     //spawns acid or heart or milk on death 
//     public void OnDied()
//     {
//         GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().IncreaseScore(pointsWorth);
//         if (Random.Range(1,heartDropChance + 1) == heartDropChance) //spawns heart
//         { 
//             Instantiate(heart, transform.position, Quaternion.identity);
//         } else if (Random.Range(1,heartDropChance + 1) == milkDropChance) //spawns milk
//         {
//             Instantiate(milk, transform.position, Quaternion.identity);
//         }
//         else //spawns acid
//         {
//             Instantiate(acid, new Vector3(transform.position.x,transform.position.y - 0.1f,0), Quaternion.identity);
//             audioHandler.GetComponents<AudioSource>()[2].Play();
//         }
        
//     }



//     Vector3 CalculateDirection()
//     {
//         // Calculate the difference between the player's position and the enemy's position
//         Vector3 direction = playerTransform.position - transform.position;

//         // Determine which axis has the greater difference
//         if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//         {
//             // Move horizontally (left or right)
//             if (direction.x > 0)
//             {
//                 animator.SetInteger("Vertical", 0);
//                 animator.SetInteger("Horizontal", -1);
//                 return Vector3.right; // Move right
//             }
//             else
//             {
//                 animator.SetInteger("Vertical", 0);
//                 animator.SetInteger("Horizontal", 1);
//                 return Vector3.left; // Move left
//             }
//         }
//         else
//         {
//             // Move vertically (up or down)
//             if (direction.y > 0)
//             {
//                 animator.SetInteger("Vertical", 1);
//                 animator.SetInteger("Horizontal", 0);
//                 return Vector3.up; // Move up
//             }
//             else
//             {
//                 animator.SetInteger("Vertical", -1);
//                 animator.SetInteger("Horizontal", 0);
//                 return Vector3.down; // Move down
//             }
//         }
//     }

//     System.Collections.IEnumerator MoveToTarget(Vector3 target)
//     {
//         isMoving = true;

//         // Move toward the target position smoothly
//         while (Vector3.Distance(transform.position, target) > 0.01f)
//         {
//             transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
//             yield return null; // Wait until the next frame
//         }

//         // Snap to the exact target position to ensure grid alignment
//         transform.position = target;

//         isMoving = false;
//     }

// }

using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform playerTransform;
    public float speed;
    public float gridSize = 0.5f;

    private Rigidbody2D rb;
    private bool isMoving = false;
    private Vector3 targetPosition;

    public Animator animator;

    public GameObject heart;
    public GameObject milk;
    public int heartDropChance = 20;
    public int milkDropChance = 20;

    public int pointsWorth = 1;

    public GameObject acid;
    private Vector2 targetDirection;
    public GameObject audioHandler;

    private float stuckTimer = 0f; // Timer to detect if stuck
    private float stuckThreshold = 1f; // Time before deciding to unstick
    private Vector3 lastPosition;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioHandler = GameObject.FindGameObjectWithTag("Audio");
    }

    void Update()
    {
        if (!isMoving)
        {
            // Check if stuck
            if (Vector3.Distance(transform.position, lastPosition) < 0.01f)
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer >= stuckThreshold)
                {
                    // Choose random movement to unstick
                    targetPosition = transform.position + GetRandomDirection() * gridSize;
                    stuckTimer = 0f; // Reset timer
                }
            }
            else
            {
                stuckTimer = 0f; // Reset if moving
                targetPosition = transform.position + CalculateDirection() * gridSize;
            }

            lastPosition = transform.position; // Update last known position
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }

    // Spawns acid, heart, or milk on death
    public void OnDied()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().IncreaseScore(pointsWorth);

        if (Random.Range(1, heartDropChance + 1) == heartDropChance)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }
        else if (Random.Range(1, heartDropChance + 1) == milkDropChance)
        {
            Instantiate(milk, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(acid, new Vector3(transform.position.x, transform.position.y - 0.1f, 0), Quaternion.identity);
            audioHandler.GetComponents<AudioSource>()[2].Play();
        }
    }

    Vector3 CalculateDirection()
    {
        Vector3 direction = playerTransform.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                if (!IsBlocked(Vector3.right)) return SetAnimationAndReturn(Vector3.right, 0, -1);
            }
            else
            {
                if (!IsBlocked(Vector3.left)) return SetAnimationAndReturn(Vector3.left, 0, 1);
            }
        }

        if (direction.y > 0)
        {
            if (!IsBlocked(Vector3.up)) return SetAnimationAndReturn(Vector3.up, 1, 0);
        }
        else
        {
            if (!IsBlocked(Vector3.down)) return SetAnimationAndReturn(Vector3.down, -1, 0);
        }

        return Vector3.zero; // Stay still if blocked
    }

    Vector3 SetAnimationAndReturn(Vector3 moveDirection, int vertical, int horizontal)
    {
        animator.SetInteger("Vertical", vertical);
        animator.SetInteger("Horizontal", horizontal);
        return moveDirection;
    }

    Vector3 GetRandomDirection()
    {
        Vector3[] possibleMoves = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        foreach (Vector3 move in possibleMoves)
        {
            if (!IsBlocked(move)) return move;
        }
        return Vector3.zero;
    }

    bool IsBlocked(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, gridSize);
        return hit.collider != null && hit.collider.CompareTag("Enemy");
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target; // Snap to position
        isMoving = false;
    }
}
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
    public float gridSize = 0.5f;

    private Rigidbody2D rb;
    private bool isMoving = false;
    private Vector3 targetPosition;

    
    

    private Vector2 targetDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

void Update()
    {
        if (!isMoving)
        {
            // Calculate the direction to move
            Vector3 direction = CalculateDirection();

            // Set the target position based on the direction and grid size
            targetPosition = transform.position + direction * gridSize;

            // Start moving toward the target position
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    Vector3 CalculateDirection()
    {
        // Calculate the difference between the player's position and the enemy's position
        Vector3 direction = playerTransform.position - transform.position;

        // Determine which axis has the greater difference
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Move horizontally (left or right)
            if (direction.x > 0)
            {
                return Vector3.right; // Move right
            }
            else
            {
                return Vector3.left; // Move left
            }
        }
        else
        {
            // Move vertically (up or down)
            if (direction.y > 0)
            {
                return Vector3.up; // Move up
            }
            else
            {
                return Vector3.down; // Move down
            }
        }
    }

    System.Collections.IEnumerator MoveToTarget(Vector3 target)
    {
        isMoving = true;

        // Move toward the target position smoothly
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }

        // Snap to the exact target position to ensure grid alignment
        transform.position = target;

        isMoving = false;
    }
}

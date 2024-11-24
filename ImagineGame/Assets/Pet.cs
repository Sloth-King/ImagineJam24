using UnityEngine;

public class Pet : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    private float moveSpeed;
    private Vector3 directionToPlayer;
    private Vector3 localScale;
    private Vector3 offset;
    private float followDistance = 1.0f; // Distance to maintain from the player
    private float stopThreshold = 0.1f; // Threshold distance to stop shaking

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = 5f;
        localScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        MovePet();
    }

    private void MovePet()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized;
        offset = -directionToPlayer * followDistance;
        Vector3 targetPosition = player.transform.position + offset;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Calculate the distance to the target position
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Check if the pet is within the stop threshold distance
        if (distanceToTarget > stopThreshold)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            // Explicitly set the pet's position to the target position to prevent small jitters
            transform.position = targetPosition;
        }
    }


    private void LateUpdate()
    {
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        else if (rb.linearVelocity.x < 0)
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
    }

}

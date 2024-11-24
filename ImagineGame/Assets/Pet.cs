using UnityEngine;

public class Pet : MonoBehaviour
{

    private Rigidbody2D rb;
    public GameObject player;
    private float moveSpeed;
    private Vector3 directionToPlayer;
    private Vector3 localScale;
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
        rb.linearVelocity = new Vector2(directionToPlayer.x , directionToPlayer.y) * moveSpeed;
    }

    private void LateUpdate()
    {
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        else if (rb.linearVelocity.x < 0)
                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

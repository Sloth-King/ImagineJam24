using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    Weapon w;

    private bool m_facingRight = true;

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        //w.firePoint.position = rb.position;
        //Flip();
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        //transform.Rotate(0f, 180f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private bool spaceActive;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isWall;
    private Collider2D coll;
    public float timer = 1f;
    private int wallJump = 0;
    private string lastWallName = "";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 1f, groundLayer);
        isWall = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, wallLayer);

        if (Input.GetButtonDown("Jump") && isGrounded && !isWall)
        {
            Vector2 jumpVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
            rb.velocity = jumpVelocity;
        }
        else if (isWall && wallJump < 3)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.2f, wallLayer);
            string currentWallName = "";
            foreach (Collider2D collider in colliders)
            {
                currentWallName = collider.gameObject.name;
            }

            if (currentWallName != lastWallName)
            {
                wallJump = 0; // Reset count if hitting a new wall
                lastWallName = currentWallName; // Update the last wall hit
            }

            spaceActive = true;
            timer -= Time.deltaTime;
            rb.gravityScale = 0f;

            if (Input.GetButtonDown("Jump") && spaceActive)
            {
                rb.gravityScale = 2f;
                Vector2 jumpVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
                rb.velocity = jumpVelocity;
                wallJump++;
                Debug.Log(wallJump);

                if (wallJump >= 3)
                {
                    rb.gravityScale = 10f;
                    rb.angularDrag = 0.1f;
                }
            }
            else if (timer <= 0.85 || !Input.GetButton("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.angularDrag = 0f;
                timer = 1f;
            }
        }
        else if (!isWall)
        {
            lastWallName = ""; // Reset last wall name when not on a wall
            rb.gravityScale = 2f;
            rb.angularDrag = 0f;
            timer = 1f;
        }

        if (isGrounded)
        {
            wallJump = 0; // Reset wall jump count when grounded
        }

        if (Input.GetKey(KeyCode.S) && isWall)
        {
            rb.angularDrag = 0.1f;
            rb.gravityScale = 10f;
        }
    }

    void OnDrawGizmos()
    {
        if (coll != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(coll.bounds.center, coll.bounds.size);
        }
    }
}
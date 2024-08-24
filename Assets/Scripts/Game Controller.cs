using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float dodgeDuration = 0.5f;
    [SerializeField] private KeyCode dodgeKey = KeyCode.E;

    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isGrounded;
    private bool isWall;
    private float timer = 2f;
    private int wallJumpCount = 0;
    private GameObject lastWall = null;
    private bool isDodging = false;
    private int playerLayer;
    private int enemyLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        Move();
        Jump();

        if (Input.GetKeyDown(dodgeKey) && !isDodging)
        {
            StartCoroutine(Dodge());
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        CheckGroundAndWallStatus();

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && !isWall)
            {
                ApplyJump();
            }
            else if (isWall && wallJumpCount < 3)
            {
                HandleWallJump();
            }
        }

        if (isGrounded) wallJumpCount = 0; // Reset wall jump count when grounded
    }

    private void CheckGroundAndWallStatus()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 1f, groundLayer);
        isWall = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, wallLayer);
    }

    private void ApplyJump()
    {
        Vector2 jumpVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
        rb.velocity = jumpVelocity;
    }

    private void HandleWallJump()
    {      
        var colliders = Physics2D.OverlapCircleAll(transform.position, 1.2f, wallLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != lastWall)
            {
                lastWall = collider.gameObject;
                wallJumpCount = 0;
            }
        }

        timer -= Time.deltaTime;
        rb.gravityScale = 0f;

        if (Input.GetButtonDown("Jump"))
        {
            rb.gravityScale = 2f;
            ApplyJump();
            wallJumpCount++;
            Debug.Log(wallJumpCount);
        }

        if (timer <= 0.65f || !Input.GetButton("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 2f;
            timer = 2f;
        }
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        yield return new WaitForSeconds(dodgeDuration);

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        isDodging = false;
    }
}
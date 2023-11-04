using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    private IMoveController moveController;
    private bool isGrounded;
    private bool canJump;
    private Rigidbody2D rb;
    private Animator animator;
    public Transform onAirCheckPoint;
    public float onAirCheckRadius;
    public float movementSpeed;
    public float jumpStrength;
    public UnityEvent onLanding;
    public UnityEvent onJump;
    public LayerMask canLandLayers;
    public float jumpInvalidatorDelay;
    public float gravityLow;
    public float gravityHigh;
    public PhysicsMaterial2D movementMaterial;
    public PhysicsMaterial2D standingMaterial;

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<IMoveController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovementUpdate();
        JumpUpdate();
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.x));
    }

    private void MovementUpdate()
    {
        Vector2 moveInput = Vector2.ClampMagnitude(moveController.getMoveInput(), 1.0f);
        if (moveInput.magnitude > 0)
        {
            rb.sharedMaterial = movementMaterial;
        }
        else
        {
            rb.sharedMaterial = standingMaterial;
        }
        rb.velocity = moveInput * movementSpeed + Vector2.up * rb.velocity.y;

        if (moveInput.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        if (moveInput.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void JumpUpdate()
    {
        bool isGroundedNow = Physics2D.OverlapCircle(onAirCheckPoint.position, onAirCheckRadius, canLandLayers);
        Debug.Log($"isGroundedNow={isGroundedNow};isGrounded={isGrounded}");
        if (!isGrounded && isGroundedNow)
        {
            isGrounded = isGroundedNow;
            canJump = true;
            onLanding.Invoke();
            animator.SetBool("IsGrounded", true);
            Debug.Log("landing");
        }
        if (isGrounded && !isGroundedNow)
        {
            Debug.Log("going on air");
            isGrounded = isGroundedNow;
            StartCoroutine(WaitSetCanJumpFalse(jumpInvalidatorDelay));
        }

        bool jumpInput = moveController.getJumpInput();
        if (canJump && jumpInput)
        {
            rb.velocity = Vector2.up * jumpStrength + Vector2.right * rb.velocity.x;
            canJump = false;
            onJump.Invoke();
            animator.SetBool("IsGrounded", false);
            Debug.Log("jump");
        }

        if (jumpInput)
        {
            rb.gravityScale = gravityLow;
        }
        else
        {
            rb.gravityScale = gravityHigh;
        }
    }
    private IEnumerator WaitSetCanJumpFalse(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canJump = false;
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}

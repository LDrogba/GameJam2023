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

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<IMoveController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovementUpdate();
        JumpUpdate();
    }

    private void MovementUpdate()
    {
        Vector2 moveInput = Vector2.ClampMagnitude(moveController.getMoveInput(), 1.0f);
        rb.velocity = moveInput * movementSpeed + Vector2.up * rb.velocity.y;
    }

    private void JumpUpdate()
    {
        Debug.Log(Physics2D.OverlapCircle(onAirCheckPoint.position, onAirCheckRadius, canLandLayers));
        bool isGroundedNow = Physics2D.OverlapCircle(onAirCheckPoint.position, onAirCheckRadius, canLandLayers);
        Debug.Log($"isGroundedNow={isGroundedNow};isGrounded={isGrounded}");
        if (!isGrounded && isGroundedNow)
        {
            isGrounded = isGroundedNow;
            canJump = true;
            onLanding.Invoke();
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
            //rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            canJump = false;
            onJump.Invoke();
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
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startPoint;
    public GameObject endPoint;
    //public GameObject[] jumpPoints;

    public float movementSpeed;
    //public float jumpActivationDist;
    //public float jumpStrength;

    //private bool isGrounded;
    //private bool canJump;

    private IMoveController moveController;
    private Rigidbody2D rb;
    Vector2 moveVector;
    void Start()
    {
        moveController = GetComponent<IMoveController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //JumpUpdate(); TO DO
        MovementUpdate();
    }
    //private void JumpUpdate()
    //{
    //    bool jump = false;
        
    //    foreach( var jumpPoint in jumpPoints )
    //    {
    //        if (Vector3.Distance(transform.position, jumpPoint.transform.position) <= jumpActivationDist)
    //            jump = true;
    //    }

    //    if (jump)
    //    {
    //        rb.velocity = Vector2.up * jumpStrength + Vector2.right * rb.velocity.x;
    //    }
    //    // ==========================================
    //    if (!isGrounded && isGroundedNow)
    //    {
    //        isGrounded = isGroundedNow;
    //        canJump = true;
    //        onLanding.Invoke();
    //        Debug.Log("landing");
    //    }
    //    if (isGrounded && !isGroundedNow)
    //    {
    //        Debug.Log("going on air");
    //        isGrounded = isGroundedNow;
    //        StartCoroutine(WaitSetCanJumpFalse(jumpInvalidatorDelay));
    //    }

    //    bool jumpInput = moveController.getJumpInput();
    //    if (canJump && jumpInput)
    //    {
    //        rb.velocity = Vector2.up * jumpStrength + Vector2.right * rb.velocity.x;
    //        //rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    //        canJump = false;
    //        onJump.Invoke();
    //        Debug.Log("jump");
    //    }

    //    if (jumpInput)
    //    {
    //        rb.gravityScale = gravityLow;
    //    }
    //    else
    //    {
    //        rb.gravityScale = gravityHigh;
    //    }
    //}
    private void MovementUpdate()
    {
        rb.velocity = moveVector * movementSpeed + Vector2.up * rb.velocity.y;

        if (transform.position.x <= startPoint.transform.position.x && transform.localScale.x < 0)
        {
            Flip();
        }
        if (transform.position.x >= endPoint.transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

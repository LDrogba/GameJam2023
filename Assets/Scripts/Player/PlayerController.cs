using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMoveController
{
    private Vector2 moveInput;
    private bool jumpInput;

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey("space");
    }
    public bool getJumpInput()
    {
        return jumpInput;
    }

    public Vector2 getMoveInput()
    {
        return moveInput;
    }
}

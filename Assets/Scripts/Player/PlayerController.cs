using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMoveController, IAttackController
{
    private Vector2 moveInput;
    private bool jumpInput;
    private bool attackInput;

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey("w");
        if (Input.GetKeyDown("space"))
        {
            attackInput = true;
        }
    }
    public bool getJumpInput()
    {
        return jumpInput;
    }

    public Vector2 getMoveInput()
    {
        return moveInput;
    }

    public bool GetAttackInput()
    {
        if (attackInput)
        {
            attackInput = false;
            return true;
        }
        return attackInput;
    }
}

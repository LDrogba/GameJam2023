using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterController : MonoBehaviour, IMoveController
{
    public float speed;
    public bool getJumpInput()
    {
        return false;
    }

    public Vector2 getMoveInput()
    {
        return Vector2.right * speed;
    }
}

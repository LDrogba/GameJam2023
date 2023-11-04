using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyControler : MonoBehaviour, IMoveController, IAttackController
{
    private Vector2 moveInput;

    private bool goingRight;
    private bool jumpInput;
    private bool attackInput;

    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject player;
    public GameObject[] jumpPoints;

    public float attackDist;
    public float jumpDist;
    public float guardingHeight = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x < startPoint.transform.position.x ||
            player.transform.position.x > endPoint.transform.position.x) && 
            math.abs(player.transform.position.y - transform.position.y) > guardingHeight) //player is not in guarding zone
        {
            if (transform.position.x <= startPoint.transform.position.x)
                goingRight = true;
            if (transform.position.x >= endPoint.transform.position.x)
                goingRight = false;
        }
        else {
            if (transform.position.x < player.transform.position.x )
                goingRight = true;
            if (transform.position.x > player.transform.position.x)
                goingRight = false;
        }

        if (goingRight)
        {
            moveInput.x = 1;
        }
        else
        {
            moveInput.x = -1;
        }
        jumpInput = jumpInRange();

        if (Vector3.Distance(player.transform.position, transform.position) <= attackDist)
        {
            Debug.Log(transform.name + "se atakuje");
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

    private bool jumpInRange()
    {
        foreach(GameObject go in jumpPoints) 
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= jumpDist)
            {
                Debug.Log(transform.name + " se skacze");
                return true;
            }
        }
        return false;
    }
}

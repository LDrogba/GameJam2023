using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    private IAttackController controller;
    public Vector2 hitScanPosition;
    public Vector2 hitScanSize;
    public LayerMask layersToHit;
    public float windup;
    public bool attacking;
    public UnityEvent onAttack;
    public UnityEvent onWindup;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<IAttackController>(); 
    }

    void FixedUpdate()
    {
        Vector2 right = transform.right;
        bool attackInput = controller.GetAttackInput();
        if (!attacking && attackInput)
        {
            StartCoroutine(PerformAttack());
        }

        //Physics2D.OverlapBoxAll()
    }
    private IEnumerator PerformAttack()
    {
        attacking = true;
        onWindup.Invoke();
        yield return new WaitForSeconds(windup);
        onAttack.Invoke();
        Vector2 turnedhitScanPostion = hitScanPosition * transform.localScale;
        Collider2D[] temp = Physics2D.OverlapBoxAll(turnedhitScanPostion + (Vector2)transform.position, hitScanSize, 0);
        attacking = false;
        Debug.Log("attacked");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 turnedhitScanPostion = hitScanPosition * transform.localScale;
        Gizmos.DrawCube(turnedhitScanPostion + (Vector2)transform.position, hitScanSize);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    private IAttackController controller;
    private Animator animator;
    public Vector2 hitScanPosition;
    public Vector2 hitScanSize;
    public LayerMask layersToHit;
    public float windup;
    public float cooldown;
    public bool attacking;
    public UnityEvent onAttack;
    public UnityEvent onWindup;
    public UnityEvent onCooldownEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<IAttackController>();
        animator = GetComponentInChildren<Animator>();
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
        animator.SetTrigger("Attack");
        Vector2 turnedhitScanPostion = hitScanPosition * transform.localScale;
        Collider2D[] temps = Physics2D.OverlapBoxAll(turnedhitScanPostion + (Vector2)transform.position, hitScanSize, 0, layersToHit);
        foreach (Collider2D temp in temps)
        {
            Damagable o;
            if (temp.TryGetComponent<Damagable>(out o))
            {
                o.Damage();
            }
        }
        yield return new WaitForSeconds(cooldown);
        attacking = false;
        onCooldownEnd.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 turnedhitScanPostion = hitScanPosition * transform.localScale;
        Gizmos.DrawCube(turnedhitScanPostion + (Vector2)transform.position, hitScanSize);
    }
}

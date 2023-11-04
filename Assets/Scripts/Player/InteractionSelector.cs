using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSelector : MonoBehaviour
{
    public float interactionRadius;
    public Vector2 interactionOffset;
    public GameObject interactionSign;
    public LayerMask interactionLayer;
    private IInteractable selected;
    // Start is called before the first frame update
    void Start()
    {
        interactionSign = GameObject.Instantiate(interactionSign);
        interactionSign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (selected != null)
            {
                selected.Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 offset = transform.localScale.x * interactionOffset;
        Collider2D[] temps = Physics2D.OverlapCircleAll(transform.position + offset, interactionRadius, interactionLayer);
        if (temps.Length == 0)
        {
            DisableSign();
            selected = null;
            return;
        }
        Collider2D closest = temps[0];
        foreach (Collider2D temp in temps)
        {
            closest = Vector3.Distance(closest.transform.position, transform.position) >
                Vector3.Distance(temp.transform.position, transform.position) ? temp : closest;
        }
        EnableSign(closest.transform.position);
        selected = closest.GetComponent<IInteractable>();
    }

    private void DisableSign()
    {
        interactionSign.SetActive(false);
    }

    private void EnableSign(Vector3 position)
    {
        interactionSign.transform.position = position;
        interactionSign.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 offset = transform.localScale.x * interactionOffset;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + offset, interactionRadius);
    }
}

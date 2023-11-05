using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var dmg = collision.gameObject.GetComponent<Damagable>();
        if (dmg != null)
        {
            dmg.Damage();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public Vector2 hitScanPosition;
    public Vector2 hitScanSize;
    public LayerMask layerToHit;
    public bool hitOnlyZombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var temps = Physics2D.OverlapBoxAll((Vector3)hitScanPosition + transform.position, hitScanSize, 0, layerToHit);
        foreach (var temp in temps)
        {
            Debug.Log(temp.gameObject.name);
            var dmg = temp.GetComponent<Damagable>();
            if (dmg)
            {
                if (hitOnlyZombie && dmg.gameObject.name != "BananaZombie")
                {
                    continue;
                }
                dmg.Damage();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube((Vector3)hitScanPosition + transform.position, hitScanSize);
    }
}

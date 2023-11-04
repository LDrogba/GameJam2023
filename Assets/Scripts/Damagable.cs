using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent onDamaged;
    public UnityEvent onKilled;
    public int hitPoints;

    public void Damage()
    {
        hitPoints--;
        onDamaged.Invoke();
        if (hitPoints == 0)
        {
            onKilled.Invoke();
        }
    }
}

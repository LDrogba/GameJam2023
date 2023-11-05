using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieFinish : MonoBehaviour
{
    public Transform zombie;
    public UnityEvent onEnter;
    public float finishRadius;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && Vector2.Distance(zombie.position, transform.position) < finishRadius)
        {
            onEnter.Invoke();
            finished = true;
        }
    }
}

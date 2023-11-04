using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(GameObject o)
    {
        GameObject.Instantiate(o, transform.position, Quaternion.identity);
    }

    public void DelayedDestroy(float sec)
    {
        Invoke("Destroy", sec);
    }

    private void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}

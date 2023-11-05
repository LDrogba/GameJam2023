using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float len, startPosition;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        len = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("start pos of " + transform.name + ": " + startPosition.ToString());
        Debug.Log("len: " + len);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (temp > startPosition + 2*len)
            startPosition += 3*len;
        else if (temp < startPosition - 2*len)
            startPosition -= 3*len;
    }
}

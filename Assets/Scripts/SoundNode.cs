using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNode : MonoBehaviour
{
    private AudioSource ass;
    public float startTime;
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
        ass.time = startTime;
    }
    private void Update()
    {
        if (!ass.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

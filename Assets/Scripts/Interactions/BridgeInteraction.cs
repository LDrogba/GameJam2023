using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BridgeInteraction : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public GameObject leftArmDest;
    public GameObject rightArmDest;
    public GameObject rootBoneDest;

    public UnityEvent onActivate;
    public UnityEvent onDisactivate;

    public bool rightBridge;

    public float speed;

    private bool isActive = false;

    private GameObject leftArmBone;
    private GameObject rightArmBone;
    private GameObject rootBone;
    private GameObject leftArmBone_startPos;
    private GameObject rightArmBone_startPos;
    private GameObject rootBone_startPos;

    private float timeCount = 2.0f;
    void Start()
    {
        leftArmBone_startPos = new GameObject();
        rightArmBone_startPos = new GameObject();
        rootBone_startPos = new GameObject();
    }


    void Update()
    {
        if(isActive && timeCount * speed < 1.0f)
        {
            timeCount = timeCount + Time.deltaTime;

            rootBone.transform.rotation = Quaternion.Lerp(rootBone_startPos.transform.rotation, rootBoneDest.transform.rotation, timeCount * speed);
            rootBone.transform.position = Vector3.Lerp(rootBone_startPos.transform.position, rootBoneDest.transform.position, timeCount * speed);

            leftArmBone.transform.rotation = Quaternion.Lerp(leftArmBone_startPos.transform.rotation, leftArmDest.transform.rotation, timeCount * speed);
            leftArmBone.transform.position = Vector3.Lerp(leftArmBone_startPos.transform.position, leftArmDest.transform.position, timeCount * speed);

            rightArmBone.transform.rotation = Quaternion.Lerp(rightArmBone_startPos.transform.rotation, rightArmDest.transform.rotation, timeCount * speed);
            rightArmBone.transform.position = Vector3.Lerp(rightArmBone_startPos.transform.position, rightArmDest.transform.position, timeCount * speed);
        }
        else if(!isActive && timeCount * speed < 1.0f)
        {
            timeCount = timeCount + Time.deltaTime;

            rootBone.transform.rotation = Quaternion.Lerp(rootBoneDest.transform.rotation, rootBone_startPos.transform.rotation, timeCount * speed);
            rootBone.transform.position = Vector3.Lerp(rootBoneDest.transform.position, rootBone_startPos.transform.position, timeCount * speed);

            leftArmBone.transform.rotation = Quaternion.Lerp(leftArmDest.transform.rotation, leftArmBone_startPos.transform.rotation, timeCount * speed);
            leftArmBone.transform.position = Vector3.Lerp(leftArmDest.transform.position, leftArmBone_startPos.transform.position, timeCount * speed);

            rightArmBone.transform.rotation = Quaternion.Lerp(rightArmDest.transform.rotation, rightArmBone_startPos.transform.rotation, timeCount * speed);
            rightArmBone.transform.position = Vector3.Lerp(rightArmDest.transform.position, rightArmBone_startPos.transform.position, timeCount * speed);

            if(timeCount * speed >= 1.0f)
            {
                onDisactivate.Invoke();
            }
        }
    }

    public void Interact()
    {
        if(leftArmBone == null)
        {
            leftArmBone = GameObject.Find("leftPlayerArmBone");
            if(leftArmBone != null )
            {
                Debug.Log("ZNALAZLEM LEFT BONE");
            }
            rightArmBone = GameObject.Find("rightPlayerArmBone");
            rootBone = GameObject.Find("rootPlayerBone");
        }
        if (!isActive)
        {
            activeBridge();
        }
        else
        {
            disactivateBridge();
        }
    }
    
    private void activeBridge()
    {
        if (timeCount * speed > 1.0f)
        {
            timeCount = 0.0f;
            isActive = true;
            leftArmBone_startPos.transform.position = leftArmBone.transform.position;
            leftArmBone_startPos.transform.rotation = leftArmBone.transform.rotation;
            rightArmBone_startPos.transform.position = rightArmBone.transform.position;
            rightArmBone_startPos.transform.rotation = rightArmBone.transform.rotation;
            rootBone_startPos.transform.position = rootBone.transform.position;
            rootBone_startPos.transform.rotation = rootBone.transform.rotation;
            onActivate.Invoke();
        }

    }
    private void disactivateBridge()
    {
        if (timeCount * speed > 1.0f)
        {
            timeCount = 0.0f;
            isActive = false;
        }

    }
}

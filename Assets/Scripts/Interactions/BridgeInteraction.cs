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
    public GameObject leftForarmDest;
    public GameObject rightForarmDest;
    public GameObject collider;

    public UnityEvent onActivate;
    public UnityEvent onDisactivate;

    public bool rightBridge;

    public float speed;

    private bool isActive = false;

    private GameObject leftArmBone;
    private GameObject rightArmBone;
    private GameObject rootBone;
    private GameObject leftForearmBone;
    private GameObject rightForearmBone;
    private GameObject leftArmBone_startPos;
    private GameObject rightArmBone_startPos;
    private GameObject rootBone_startPos;
    private GameObject leftForearmBone_startPos;
    private GameObject rightForearmBone_startPos;
    private GameObject player;

    private float timeCount = 2.0f;
    void Start()
    {
        leftArmBone_startPos = new GameObject();
        rightArmBone_startPos = new GameObject();
        rootBone_startPos = new GameObject();
        leftForearmBone_startPos = new GameObject();
        rightForearmBone_startPos = new GameObject();
    player = GameObject.FindGameObjectWithTag("Player");
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

            rightForearmBone.transform.rotation = Quaternion.Lerp(rightForearmBone_startPos.transform.rotation, rightForarmDest.transform.rotation, timeCount * speed);
            leftForearmBone.transform.rotation = Quaternion.Lerp(leftForearmBone_startPos.transform.rotation, leftForarmDest.transform.rotation, timeCount * speed);
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

            rightForearmBone.transform.rotation = Quaternion.Lerp(rightForarmDest.transform.rotation, rightForearmBone_startPos.transform.rotation, timeCount * speed);
            leftForearmBone.transform.rotation = Quaternion.Lerp(leftForarmDest.transform.rotation, leftForearmBone_startPos.transform.rotation, timeCount * speed);

            if (timeCount * speed >= 1.0f)
            {
                player.GetComponent<Rigidbody2D>().simulated = true;
                player.GetComponent<Movement>().enabled = true;
                player.GetComponentInChildren<Animator>().enabled = true;
                collider.SetActive(false);
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
            leftForearmBone = GameObject.Find("banana_0/bone_1/bone_3");
            rightForearmBone = GameObject.Find("rootPlayerBone/bone_4");
            if (leftForearmBone != null)
            {
                Debug.Log("leftForearmBone");
            }
            if (rightForearmBone != null)
            {
                Debug.Log("ZNALAZLEM rightForearmBone");
            }

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
            Debug.Log("bridge on");
            timeCount = 0.0f;
            isActive = true;
            leftArmBone_startPos.transform.position = leftArmBone.transform.position;
            leftArmBone_startPos.transform.rotation = leftArmBone.transform.rotation;
            rightArmBone_startPos.transform.position = rightArmBone.transform.position;
            rightArmBone_startPos.transform.rotation = rightArmBone.transform.rotation;
            rootBone_startPos.transform.position = rootBone.transform.position;
            rootBone_startPos.transform.rotation = rootBone.transform.rotation;
            leftForearmBone_startPos.transform.rotation = leftForearmBone.transform.rotation;
            rightForearmBone_startPos.transform.rotation = rightForearmBone.transform.rotation;

            player.GetComponent<Rigidbody2D>().simulated = false;
            player.GetComponent<Movement>().enabled = false;
            player.GetComponentInChildren<Animator>().enabled = false;
            collider.SetActive(true);

            onActivate.Invoke();
        }

    }
    private void disactivateBridge()
    {
        if (timeCount * speed > 1.0f)
        {
            Debug.Log("bridge off");
            timeCount = 0.0f;
            isActive = false;
        }

    }
}

using game4automation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackingData : MonoBehaviour
{//This Script is for tracking the carts by spawning a gameobject when they pass a sensor
    [Header("Factory Machine")]
    public string factoryMachineID;
    public OPCUA_Interface Interface;


    [Header("OPCUA Reader")]
    public string nodeBeingMonitored;
    public string nodeID;

    //public TMP_Text digitalTwinFeedbackTMP;
    //public TMP_Text uiFeedbackTMP;
    public string dataFromOPCUANode;
    public GameObject prefabToSpawn;
    public float despawnTime = 1f;
    public float startTime = 4f;
    private float timer = 0.0f;
    public GameObject SpawnPoint;

    public Vector3 targetPosition;
    public float moveTime = 1f;

    public TMP_Text uiFeedbackTMP;
    void Start()
    {
        Interface.EventOnConnected.AddListener(OnInterfaceConnected);
        Interface.EventOnConnected.AddListener(OnInterfaceDisconnected);
        Interface.EventOnConnected.AddListener(OnInterfaceReconnect);
    }


    private void OnInterfaceConnected()
    {
        Debug.LogWarning("Connected to Factory Machine " + factoryMachineID);
        var subscription = Interface.Subscribe(nodeID, NodeChanged);
        dataFromOPCUANode = subscription.ToString();
        Debug.LogError(dataFromOPCUANode);
        //digitalTwinRFIDFeedbackTMP.text = RFIDInfo;
        //uiRFIDFeedbackTMP.text = RFIDInfo;        
    }

    private void OnInterfaceDisconnected()
    {
        Debug.LogWarning("Factory Machine " + factoryMachineID + " has disconnected");
    }

    private void OnInterfaceReconnect()
    {
        Debug.LogWarning("Factory Machine " + factoryMachineID + " has reconnected");
    }

    public void NodeChanged(OPCUANodeSubscription sub, object value)
    {
        dataFromOPCUANode = value.ToString();
        Debug.LogError("Factory machine " + factoryMachineID + " just registered " + nodeBeingMonitored + " as " + dataFromOPCUANode);
    }



    private void Update()
    {
        {
            timer += Time.deltaTime;

            if (timer >= startTime)
            {
                timer = 0.0f; //This is not working as intended, timing is not right

                uiFeedbackTMP.text = dataFromOPCUANode;
                //if (dataFromOPCUANode == "1")
                //{
                    //Debug.Log("1 at machine 1");
                    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                StartCoroutine(MoveObjectOverTime());

                StartCoroutine(CountdownDestroy(newPrefab));
            }
        }
    }
    public IEnumerator CountdownDestroy(GameObject ObjectToDestroy) 
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(ObjectToDestroy);
    }
    public IEnumerator MoveObjectOverTime() //This script is my attempt to make the gameobjects to move when they spawn but does not work
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}


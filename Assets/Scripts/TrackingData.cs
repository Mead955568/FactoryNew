using game4automation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackingData : MonoBehaviour
{
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
                timer = 0.0f;

                uiFeedbackTMP.text = dataFromOPCUANode;
                //if (dataFromOPCUANode == "1")
                //{
                    //Debug.Log("1 at machine 1");
                    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                StartCoroutine(MoveObjectOverTime());

                StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "2")
                //{
                //    Debug.Log("2 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "3")
                //{
                //    Debug.Log("3 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "4")
                //{
                //    Debug.Log("4 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "5")
                //{
                //    Debug.Log("5 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "6")
                //{
                //    Debug.Log("6 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "7")
                //{
                //    Debug.Log("7 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "8")
                //{
                //    Debug.Log("8 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "9")
                //{
                //    Debug.Log("9 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //    StartCoroutine(CountdownDestroy(newPrefab));
                //}
                //if (dataFromOPCUANode == "10")
                //{
                //    Debug.Log("10 at machine 1");
                //    GameObject newPrefab = Instantiate(prefabToSpawn, SpawnPoint.transform.position, Quaternion.identity);
                //StartCoroutine(CountdownDestroy(newPrefab));
                //}
            }
        }
    }
    public IEnumerator CountdownDestroy(GameObject ObjectToDestroy) 
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(ObjectToDestroy);
    }
    public IEnumerator MoveObjectOverTime()
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


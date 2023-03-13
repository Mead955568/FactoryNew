using game4automation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaftyCheck : MonoBehaviour
{
[Header("Factory Machine")]
public string factoryMachineID;
public OPCUA_Interface Interface;


[Header("OPCUA Reader")]
public string nodeBeingMonitored;
public string nodeID;

//public TMP_Text digitalTwinFeedbackTMP;
public TMP_Text uiFeedbackTMP;
public string dataFromOPCUANode;
public AudioSource error;

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
    //uiFeedbackTMP.text = "Factory machine " + factoryMachineID + " Safty Check " + nodeBeingMonitored + " as " + dataFromOPCUANode;

        if (dataFromOPCUANode == "False")
        {
            if (!error.isPlaying)
            {
                error.Play();
                Debug.Log("Hand in Machine");
            }
        }
        else if (dataFromOPCUANode == "True")
        {
            
            Debug.Log("No Hand in Machine");
        }
        else
        {
            Debug.Log("Not Working");
        }
    }
}
// ns = 3; s = "xBG_BG2.Q1" This is a the data point needed to do the safty check for True or False
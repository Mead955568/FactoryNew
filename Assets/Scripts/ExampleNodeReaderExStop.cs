using UnityEngine;
using game4automation;
using TMPro;

public class ExampleNodeReaderExStop : MonoBehaviour
{

    [Header("Factory Machine")]
    public string factoryMachineID;
    public OPCUA_Interface Interface;


    [Header("OPCUA Reader")]
    public string nodeBeingMonitored;
    public string nodeID;

    //public TMP_Text digitalTwinFeedbackTMP;
    public GameObject uiColourObject;
    public string dataFromOPCUANode;
    //Colours
    public Color goodColour;
    public Color badColour;
    MeshRenderer myRenderer;

    void Start()
    {
        Interface.EventOnConnected.AddListener(OnInterfaceConnected);
        Interface.EventOnConnected.AddListener(OnInterfaceDisconnected);
        Interface.EventOnConnected.AddListener(OnInterfaceReconnect);
        myRenderer = GetComponent<MeshRenderer>();
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
    { //This is checking if the Emergency Stop button has been pressed
        if (dataFromOPCUANode == "False")
        {
            myRenderer.material.color = badColour;
            Debug.Log("Bad Colour ON");
        }
        else if (dataFromOPCUANode == "True")
        {
            myRenderer.material.color = goodColour;
            Debug.Log("Good Colour ON");
        }
        else
        {
            Debug.Log("No Color On");
        }
    }
}
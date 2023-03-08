using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

/// <summary> 
/// TCP Client-Server Connection Example.
/// 2 way communication: Client (your app) connects to the server and can send and receive messages.
/// Simply place this script on an empty gameobject in your scene. Note your firewall may need to be disabled.
/// The OrderCompleteNewPhone method is an example of how you could use it. e.g calling it from a UI button.
/// <summary> 

public class OrderOptions : MonoBehaviour
{
    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion

    #region public members 	

    // Address of the server PC connected to the Festo machine. This should not need to be changed.
    public string serverAddress = "172.21.0.90";

    // Example of a message to send to the MES server.
    // This message places a new order of the complete mobile phone (known as part number 210). See documentation on Canvas for full breakdown of how this string is formatted.
    // Additional part numbers can be found on the Festo PC.   

    public string partNumber1;
    public string partNumber2;
    public string partNumber3;
    public string partNumber4;
    public string partNumber5;
    public string partNumber6;
    public string partNumber7;
    public string partNumber8;

    public string qty;

    public string newOrderMessage;
    #endregion




    // Use this for initialization 	
    void Start()
    {
        ConnectToTcpServer();
    }

    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incoming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient(serverAddress, 2000);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incoming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incomingData);

                        // this is the message the MES server sends back. Its formatting is the same as the message you send to it. 
                        Debug.Log("Server message received as: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    private void SendMessageToServer(string message)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = message;
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Message has been sent by client - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    /// <summary> 
    // Call this method from somewhere in your app to place a new order
    /// <summary> 
    public void SendOrder1ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber1 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("210");
    }
    public void SendOrder2ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber2 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("214");
    }
    public void SendOrder3ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber3 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("1200");
    }
    public void SendOrder4ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber4 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("1201");
    }
    public void SendOrder5ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber5 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("1210");
    }
    public void SendOrder6ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber6 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("3001");
    }
    public void SendOrder7ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber7 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("3002");
    }
    public void SendOrder8ToFactory()
    {
        newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo= " + partNumber8 + ";#Aux1Int=" + qty.ToString() + "\r";
        SendMessageToServer(newOrderMessage);
        Debug.Log("3003");
    }

    //  private void Update()
    //  {
    //      if(Input.GetKey(KeyCode.Space))
    //      {
    //	OrderCompleteNewPhone();
    //}
    //  }
}
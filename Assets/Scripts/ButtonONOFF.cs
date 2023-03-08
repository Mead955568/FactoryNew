using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonONOFF : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;

    public Button closeButton;


    void Start()
    {

        // Add listener to close button
        closeButton.onClick.AddListener(CloseCanvas);
    }


    void CloseCanvas()
    {
        // Disable the canvas and set it to be invisible
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
    }
}

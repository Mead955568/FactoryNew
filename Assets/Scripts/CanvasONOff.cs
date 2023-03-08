using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasONOff : MonoBehaviour
{
    public GameObject Panal;
    public Button openButton;
    public Button closeButton;

    void Start()
    {
        // Add listener to open button
        openButton.onClick.AddListener(OpenCanvas);

        // Add listener to close button
        closeButton.onClick.AddListener(CloseCanvas);
    }

    void OpenCanvas()
    {
        // Enable the canvas and set it to be visible
        Panal.gameObject.SetActive(true);
    }

    void CloseCanvas()
    {
        // Disable the canvas and set it to be invisible
        Panal.gameObject.SetActive(false);
    }
}

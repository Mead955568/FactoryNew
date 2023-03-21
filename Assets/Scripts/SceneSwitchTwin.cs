using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitchTwin : MonoBehaviour
{// This script just switchs the scene between the Twin and back to the camera
    public GameObject Cam1;
    public GameObject Cam2;

    public Button switchTwin;
    public GameObject Button;
    public GameObject Button2;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = switchTwin.GetComponent<Button>();
        btn.onClick.AddListener(SwitchTwin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchTwin()
    {
        Debug.Log("Switch Twin");
        Cam1.SetActive(true);
        Cam2.SetActive(false);

        Button.SetActive(false);
        Button2.SetActive(true);
    }
}

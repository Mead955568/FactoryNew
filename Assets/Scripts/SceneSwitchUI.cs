using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitchUI : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;

    public Button switchUI;
    public GameObject Button;
    public GameObject Button2;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = switchUI.GetComponent<Button>();
        btn.onClick.AddListener(SwitchUI);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchUI()
    {
        Debug.Log("Switch UI");
        Cam1.SetActive(false);
        Cam2.SetActive(true);

        Button.SetActive(false);
        Button2.SetActive(true);
    }
}

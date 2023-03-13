using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class FinishedOrders : MonoBehaviour
{
    public List<string> FinishedOrderData = new List<string>();

    public FinishedOrdersJson[] finishedOrdersObjectArray;

    public string listInfo;

    public TMP_Text info;

    public void ReceieveData(string FinishedOrderStringPHPMany)
    {
        string newFinishedOrderStringPHPMany = fixJson(FinishedOrderStringPHPMany);

        Debug.LogWarning(newFinishedOrderStringPHPMany);

        finishedOrdersObjectArray = JsonHelper.FromJson<FinishedOrdersJson>(newFinishedOrderStringPHPMany);

        FinishedOrderData.Clear();
        listInfo = "";

        for (int i = 0; i < finishedOrdersObjectArray.Length; i++)
        {
            Debug.LogWarning("ONo:" + finishedOrdersObjectArray[i].ONo + ", Company:" + finishedOrdersObjectArray[i].Company + ", Planned Start:" + finishedOrdersObjectArray[i].PlannedStart + ", Planned End:" + finishedOrdersObjectArray[i].PlannedEnd + ", State:" + finishedOrdersObjectArray[i].State);

            FinishedOrderData.Add("Order Number: " + finishedOrdersObjectArray[i].ONo + ", Company Name: " + finishedOrdersObjectArray[i].Company + ", Planned Start Time: " + finishedOrdersObjectArray[i].PlannedStart + ", Planned End Time: " + finishedOrdersObjectArray[i].PlannedEnd + ", Build State: " + finishedOrdersObjectArray[i].State);
        }

        foreach (var listMember in FinishedOrderData)
        {
            listInfo += listMember.ToString() + "\n" + "\n";
        }

        info.text = listInfo;
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public void GetRequestPublic()
    {
        StartCoroutine(GetRequest("http://172.21.0.90/SQLData.php?Command=finishedOrders? numOrders=100 "));     //calls coroutine and sets string
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    ReceieveData(webRequest.downloadHandler.text);
                    Debug.LogError("Finished Orders Success");

                    break;
            }
        }
    }
}

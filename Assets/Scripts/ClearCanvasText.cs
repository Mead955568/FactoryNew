using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearCanvasText : MonoBehaviour
{
    public TMP_Text uiFeedbackTMP1;
    public TMP_Text uiFeedbackTMP2;
        public TMP_Text uiFeedbackTMP3;

    // Clear the text in the canvas
    public void ClearText()
    {
        uiFeedbackTMP1.text = "Finished Order Info";
        uiFeedbackTMP2.text = "Current Order Info";
        uiFeedbackTMP3.text = "Connection Check";
    }
}

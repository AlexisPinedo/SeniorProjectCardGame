using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputManager : MonoBehaviour
{
    private List<Button> buttonList = new List<Button>();
    
    //private static ButtonInputManager

    public void DisableButtonsInList()
    {
        foreach (Button aButton in buttonList)
        {
            aButton.enabled = false;
        }
    }

    public void EnableButtonsInList()
    {
        foreach (Button aButton in buttonList)
        {
            aButton.enabled = true;
        }
    }
}

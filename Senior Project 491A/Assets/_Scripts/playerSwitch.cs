using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSwitch : MonoBehaviour
{
    public GameObject p1HandSpacePanel;
    public GameObject p2HandSpacePanel;

    //private void OnEnable()
    //{
    //    ButtonInputEventHandler.StartClicked += test;
    //}

    //void test() {

    //    Debug.Log("Button pressed");
    
    //}


    void Start()
    {
        p2HandSpacePanel.SetActive(false);
    }

    public void ShowHidePanel()
    {
        if (p1HandSpacePanel != null && p2HandSpacePanel != null)
        {
            if (p1HandSpacePanel.activeSelf)
            {
                p1HandSpacePanel.SetActive(false);
                p2HandSpacePanel.SetActive(true);
            }

            else if (p2HandSpacePanel.activeSelf)
            {
                p2HandSpacePanel.SetActive(false);
                p1HandSpacePanel.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanel : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject CreditsPanel;


    public void OpenMainPanel()
    {
        CreditsPanel.SetActive(false);
        MainPanel.SetActive(true);


    }

    public void OpenCreditsPanel()
    {
        MainPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

}

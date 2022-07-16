using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchPanel : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject CreditsPanel;

    private bool TextIsScrolling;

    private void Start()
    {
        TextIsScrolling = false;
    }

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

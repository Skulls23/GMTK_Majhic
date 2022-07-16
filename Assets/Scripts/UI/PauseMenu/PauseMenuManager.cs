using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject pausePanel;
    
    private void Open()
    {
        isOpen = true;
        pausePanel.SetActive(true);
    }

    private void Close()
    {
        isOpen = false;
        pausePanel.SetActive(false);
    }

    public void OpenClose()
    {
        if (isOpen) Close();
        else Open();
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}

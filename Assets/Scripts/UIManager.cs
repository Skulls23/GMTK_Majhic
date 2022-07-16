using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public GameObject forgePanel;
    
    public void OpenForgePanel()
    {
        forgePanel.SetActive(true);
    }

    public void CloseForgePanel()
    {
        forgePanel.SetActive(false);
    }
}

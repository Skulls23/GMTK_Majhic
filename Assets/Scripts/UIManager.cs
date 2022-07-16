using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [HideInInspector] public bool isForgeOpen;
    public Dice dice;

    void Awake()
    {
        Instance = this;
    }

    public ForgeHandler forgePanel;
    
    public void OpenForgePanel()
    {
        forgePanel.Open();
        isForgeOpen = true;
    }

    public void CloseForgePanel()
    {
        forgePanel.Close();
        isForgeOpen = false;
    }
}

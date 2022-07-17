using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [HideInInspector] public bool isForgeOpen;
    public Dice dice;
    public TextMeshProUGUI roundText;
    public Texture2D cursor;

    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

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

    public void UpdateRoundText()
    {
        roundText.text = "Round " + GameManager.Instance.currentRound;
    }
}

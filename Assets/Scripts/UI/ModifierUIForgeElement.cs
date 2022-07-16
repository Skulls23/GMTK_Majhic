using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierUIForgeElement : MonoBehaviour
{
    public Image modifierImage;
    public ModifiersData modifierData;
    public Button submitButton;

    public bool isSelectedForNextWave = false;

    void Start()
    {
        submitButton.onClick.AddListener(() => 
        {
            UIManager.Instance.forgePanel.DisplayModifierInfo(modifierData, isSelectedForNextWave);
        });
    }   

    public void SetupElement(ModifiersData p_modifier)
    {
        modifierImage.sprite = p_modifier.modifierVisual;
        modifierData = p_modifier;
    }
}

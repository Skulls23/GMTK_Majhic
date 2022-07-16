using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ForgeHandler : MonoBehaviour
{
    [Header("Containers")]
    public GameObject diceContainer;
    public GameObject modifiersContainer;
    public GameObject activeEffectsContainer;
    public GameObject descriptionContainer;
    public Button validateButton;

    [Header("Variable Texts")]
    public TextMeshProUGUI selectedModifierTitle;
    public TextMeshProUGUI selectedModifierDescription;
    public TextMeshProUGUI activeEffectsList;

    void Start()
    {
        validateButton.onClick.AddListener(() => 
        {
            if(ModifierManager.Instance.currentForgeSelectedModifier != null)
            {
                AddTargetModifier();
                UIManager.Instance.CloseForgePanel();
            }
        });
    }

    public void Open()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1.0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        ModifierManager.Instance.currentForgeSelectedModifier = null;
        GetModifiers();
        SetupDiceFaces();
    }

    void GetModifiers()
    {
        for(int i = 0,n = modifiersContainer.transform.childCount; i < n; i++)
        {
            ModifiersData randomMod = ModifierManager.Instance.GetARandomModifierFromDatabase();
            modifiersContainer.transform.GetChild(i).GetChild(0).GetComponent<ModifierUIForgeElement>().SetupElement(randomMod);
        }
    }

    public void Close()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 0.0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        selectedModifierTitle.text = "Modifier";
        selectedModifierDescription.text = "Select a new Modifier.";
    }

    public void DisplayModifierInfo(ModifiersData p_modifiersData, bool isSelectedForNextWave)
    {
        if(isSelectedForNextWave)
            ModifierManager.Instance.currentForgeSelectedModifier = p_modifiersData;

        selectedModifierTitle.text = p_modifiersData.modifierDisplayName;
        selectedModifierDescription.text = p_modifiersData.modifierDescription;
    }

    void SetupDiceFaces()
    {
        for(int i = 0, n = diceContainer.transform.childCount; i < n; i++)
        {
            ModifiersData mod = UIManager.Instance.dice.GetSide(i);
            diceContainer.transform.GetChild(i).GetComponent<ModifierUIForgeElement>().SetupElement(mod);
        }
    }

    void AddTargetModifier()
    {
        int rndDiceFace = Random.Range(0,6);
        UIManager.Instance.dice.SetSide(rndDiceFace, ModifierManager.Instance.currentForgeSelectedModifier);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForgeHandler : MonoBehaviour
{
    [Header("Containers")]
    public GameObject diceContainer;
    public GameObject modifiersContainer;
    public GameObject activeEffectsContainer;
    public GameObject descriptionContainer;

    [Header("Modifiers")]
    public GameObject modifier1;
    public GameObject modifier2;
    public GameObject modifier3;

    [Header("Variable Texts")]
    public TextMeshProUGUI selectedModifierTitle;
    public TextMeshProUGUI selectedModifierDescription;
    public TextMeshProUGUI activeEffectsList;

    [Header("Active Faces")]
    public GameObject face1;
    public GameObject face2;
    public GameObject face3;
    public GameObject face4;
    public GameObject face5;
    public GameObject face6;
}

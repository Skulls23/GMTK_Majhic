using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(fileName = "New modifiers", menuName = "ScriptableObjects")]
[SerializeField] public class ModifiersData : ScriptableObject
{
    [Header("Modifiers global info")]
    public string modifierDisplayName;
    public int numberOfWaveBeforeSpawning;
    public Sprite modifierVisual;
    public bool replacementPriority; // when used by the dice, this modifier have a great chance of being replaced by another one

    [Space(10)]
    [Header("Modifier Apply ")]
    public TargetToApply[] targetToApply;
    public enum TargetToApply {Player, Enemy, None};

    public enum UpgradeManifestation
    {
        OnCreated,
        OnAreaEnter,
        OnBeingAlone,
        OnAddEffetOnBullet, 
        OnDeath,
        OnHit 
    }
    public UpgradeManifestation upgradeManifestation;
    public GameObject[] objectToCreatePrefab;
    public List<ActionModifier> actionModifierList;
}

[Serializable]public class ActionModifier
{
    public enum Action{Create, ChangeStat, CallAMethod};
    public Action action;
    public string Arg1;
    public string Arg2;
    public string Arg3;
    public string Arg4;
}

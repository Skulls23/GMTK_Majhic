using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using System;

public class ModifierManager : MonoBehaviour
{
    [HideInInspector] public ModifiersData currentForgeSelectedModifier; // le dernier modifier acceptable selectionné par le joueur pendant la phase de forge

    public static ModifierManager Instance;
    public List<ModifiersData> enemiesModifierList;
    public List<ModifiersData> playerModifierList;
    public List<ModifiersData> worldModifiers; // used for unit spawn and stuff like that
    public List<ModifiersData> allModifiersList;

    void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        UnitsManager.Instance.onUnitDeath += ActionOnUnitDeath;

        if (enemiesModifierList == null)
            enemiesModifierList = new List<ModifiersData>();

        if (playerModifierList == null)
            playerModifierList = new List<ModifiersData>();

        if (worldModifiers == null)
            worldModifiers = new List<ModifiersData>();
    }

    private void ActionOnUnitDeath(/*UnitHandler unitDead*/)
    {
        // on stuff when a unit die
    }

    public void AddModifier(ModifiersData mod)
    {
        if (mod.targetToApply.Contains(ModifiersData.TargetToApply.Player)) AddPlayerModifier(mod);
        if (mod.targetToApply.Contains(ModifiersData.TargetToApply.Enemy)) AddEnemiesModifier(mod);
        if (mod.targetToApply.Contains(ModifiersData.TargetToApply.None)) AddWorldModifier(mod);
        if (mod.targetToApply.Contains(ModifiersData.TargetToApply.Enemy)) throw new Exception("Global Modifiers not implemented");
    }

    public void AddPlayerModifier(ModifiersData mod)
    {
        playerModifierList.Add(mod);
    }

    public void AddEnemiesModifier(ModifiersData mod)
    {
        enemiesModifierList.Add(mod);
    }

    public void AddWorldModifier(ModifiersData mod)
    {
        worldModifiers.Add(mod);
    }

    public List<ModifiersData> GetAllPlayerModifier()
    {
        return playerModifierList;
    }

    public List<ModifiersData> GetAllEnemiesModifier()
    {
        return enemiesModifierList;
    }

    public List<ModifiersData> GetAllWorldModifier()
    {
        return worldModifiers;
    }

    public ModifiersData GetARandomModifierFromDatabase()
    {
        int rnd = Random.Range(0, allModifiersList.Count);
        return allModifiersList[rnd];
    }

    public void ResetUpgradeManager()
    {
        enemiesModifierList.Clear();
        playerModifierList.Clear();
    }

}

using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Debug=UnityEngine.Debug;
using Random=UnityEngine.Random;
using System;

public class ModifierManager : MonoBehaviour
{
    public static ModifierManager Instance;
    private List<ModifiersData> enemiesModifierList;
    private List<ModifiersData> playerModifierList;

    void Awake()
    {
        Instance = this;
        UnitsManager.Instance.onUnitDeath += ActionOnUnitDeath;

        if(enemiesModifierList == null)
            enemiesModifierList = new List<ModifiersData>();

        if(playerModifierList == null)
            playerModifierList = new List<ModifiersData>();
    }

    private void ActionOnUnitDeath(/*UnitHandler unitDead*/)
    {
        // on stuff when a unit die
    }

    public void AddPlayerModifier(ModifiersData mod)
    {
        playerModifierList.Add(mod);
    }

    public void AddEnemiesModifier(ModifiersData mod)
    {
        enemiesModifierList.Add(mod);
    }

    public List<ModifiersData> GetAllPlayerModifier()
    {
        return playerModifierList;
    }

    public List<ModifiersData> GetAllEnemiesModifier()
    {
        return enemiesModifierList;
    }

    public void ResetUpgradeManager()
    {
        enemiesModifierList.Clear();
        playerModifierList.Clear();
    }
}

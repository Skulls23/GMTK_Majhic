using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class ModifierHandler : MonoBehaviour
{
    public UnitStats unitStats;
    private List<ModifiersData> modifiersToApply;

    public ModifiersData.TargetToApply team;

    void Start()
    {
        unitStats = GetComponent<UnitStats>();
        modifiersToApply = new List<ModifiersData>();

        if (team == ModifiersData.TargetToApply.Player)
        {
            modifiersToApply = ModifierManager.Instance.GetAllPlayerModifier();
            Subscription();
        }
        if (team == ModifiersData.TargetToApply.Enemy)
        {
            modifiersToApply = ModifierManager.Instance.GetAllEnemiesModifier();
            Subscription();
        }

        if(unitStats != null)
            unitStats.onCreated();
    }

    public void Subscription()
    {
        unitStats.onCreated -= ActionOnCreation;
        unitStats.onDeath -= ActionOnBeingDead;
        unitStats.onTakeDamage -= ActionOnBeingHit;
        unitStats.onShoot -= ActionOnBulletShoot;

        unitStats.onCreated += ActionOnCreation;
        unitStats.onDeath += ActionOnBeingDead;
        unitStats.onTakeDamage += ActionOnBeingHit;
        unitStats.onShoot += ActionOnBulletShoot;
    }

    public void ActionOnCreation()
    {
        List<ModifiersData> modifiersToDoOnCreation = modifiersToApply.FindAll(x => x.upgradeManifestation == ModifiersData.UpgradeManifestation.OnCreated);
        foreach (ModifiersData mod in modifiersToDoOnCreation)
        {
            foreach (ActionModifier action in mod.actionModifierList)
            {
                ReadSkill(action, mod.name, mod.objectToCreatePrefab);
            }
        }
    }

    public void ActionOnBulletShoot(/*bullet shot*/)
    {
        // a faire
    }

    public void ActionOnBeingDead()
    {
        List<ModifiersData> modifiersToDoOnDeath = modifiersToApply.FindAll(x => x.upgradeManifestation == ModifiersData.UpgradeManifestation.OnDeath);
        foreach (ModifiersData mod in modifiersToDoOnDeath)
        {
            foreach (ActionModifier actionSkill in mod.actionModifierList)
            {
                ReadSkill(actionSkill, mod.name, mod.objectToCreatePrefab);
            }
        }
    }

    public void ActionOnBeingHit(int amount, Vector3 attackPos)
    {
        List<ModifiersData> modifiersToDoOnHit = modifiersToApply.FindAll(x => x.upgradeManifestation == ModifiersData.UpgradeManifestation.OnHit);
        foreach (ModifiersData mod in modifiersToDoOnHit)
        {
            foreach (ActionModifier actionSkill in mod.actionModifierList)
            {
                ReadSkill(actionSkill, mod.name, mod.objectToCreatePrefab);
            }
        }
    }

    public void ReadSkill(ActionModifier actionModifier, string modifierName = "", GameObject[] p_objToCreatePrefab = null)
    {
        switch (actionModifier.action)
        {
            case ActionModifier.Action.ChangeStat: // action: Change Stat, Arg1: variable name, Arg2 : amountToAdd/Remove(eg. '-255', '+5', '10%'), Arg3 : Duration
                ChangeUnitStat(actionModifier, modifierName);
                break;
            case ActionModifier.Action.Create: // arg1 = index of object to create in object to create prefab[]

                if (p_objToCreatePrefab != null)
                {
                    GameObject go;
                    go = Instantiate(p_objToCreatePrefab[int.Parse(actionModifier.Arg1)], transform.position, Quaternion.identity);
                }
                break;
            case ActionModifier.Action.CallAMethod: // arg1 = method name, Arg2->4 : method args
                unitStats.Invoke(actionModifier.Arg1, 0.0f);
                break;
        }
    }
    static BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public void ChangeUnitStat(ActionModifier actionModifier, string modifierName)
    {
        Debug.Log("Change unit Stat");
        UnitStats whoToChangeStat = unitStats;

        // on récupére la property

        PropertyInfo pi = whoToChangeStat.GetType().GetProperty(actionModifier.Arg1, FLAGS);

        bool isPercentage = actionModifier.Arg2.Substring(actionModifier.Arg2.Length - 1) == "%" ? true : false;

        string statValue = actionModifier.Arg2;

        if (isPercentage)
            statValue = actionModifier.Arg2.Substring(0, actionModifier.Arg2.Length - 1);


        // on tf le string qui contient la valeur en float
        float arg2ToFloat = float.Parse(statValue);

        if (string.IsNullOrEmpty(actionModifier.Arg3))
        {
            //si arg3 = empty, alors c est une stat qui change instantannément ex. les points de vie qui baissent
            if (pi.GetValue(whoToChangeStat).GetType() == typeof(float))
            {
                if (isPercentage)
                {
                    // formule du pourcentage : x + 10% => x * (1.0 + 10/100)
                    // formule du pourcentage : x - 10% => x * (1.0 + -10/100)
                    arg2ToFloat = ((float)pi.GetValue(whoToChangeStat) * (arg2ToFloat / 100));
                }

                Debug.Log("Change stat : "+actionModifier.Arg1+" on : "+whoToChangeStat.gameObject.name);

                pi.SetValue(whoToChangeStat, ((float)pi.GetValue(whoToChangeStat) + arg2ToFloat));
            }
            else // si pas float (donc int)
            {
                if (isPercentage)
                {
                    // formule du pourcentage : x + 10% => x * (1.0 + 10/100)
                    // formule du pourcentage : x - 10% => x * (1.0 + -10/100)
                    arg2ToFloat = ((int)pi.GetValue(whoToChangeStat) * (arg2ToFloat / 100f));
                }
                pi.SetValue(whoToChangeStat, ((int)pi.GetValue(whoToChangeStat) + (int)arg2ToFloat));
            }
        }

        // HERE CALL FUNCT TO DISPLAY WHAT HAPPENED (affichage qui montre quelle stat est ajoutée si pour le joueur par exemple?)
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance;
    public int maxEnemyAmountOnScreen;
    public delegate void OnUnitDeath();
    public OnUnitDeath onUnitDeath;

    public List<GameObject> enemyToCreateEachWave;
    public int amountOfRemainingEnemy;
    public Transform enemyParent;
    public Transform[] spawnerPoints;


    void Awake()
    {
        Instance = this;   
    }

    public void Init()
    {
        enemyToCreateEachWave = new List<GameObject>();
    }

    public void AddEnemyToWave(GameObject enemy)
    {
        enemyToCreateEachWave.Add(enemy);
    }

    public void StartWave()
    {
        CreateEnemies();
    }

    void CreateEnemies()
    {
        List<ModifiersData> modifiers = ModifierManager.Instance.GetAllWorldModifier().FindAll(x=>x.upgradeManifestation == ModifiersData.UpgradeManifestation.OnCreated);
        foreach(ModifiersData mod in modifiers)
        {
            CreateEnemy(mod);
        }
    }

    void CreateEnemy(ModifiersData mod)
    {
        foreach(ActionModifier action in mod.actionModifierList)
        {
            if(action.action == ActionModifier.Action.Create)
            {   
                Transform tf = GetRandomSpawner();
                GameObject go = Instantiate(mod.objectToCreatePrefab[0], tf.position, Quaternion.identity);
                go.transform.SetParent(enemyParent);
                amountOfRemainingEnemy ++;
            }
        }
    }

    Transform GetRandomSpawner()
    {
        int rnd = Random.Range(0, spawnerPoints.Length);
        return spawnerPoints[rnd];
    }
}

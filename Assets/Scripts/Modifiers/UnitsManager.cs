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

    }
}

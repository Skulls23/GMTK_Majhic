using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public int maxEnemyAmountOnScreen;
    public static UnitsManager Instance;
    public delegate void OnUnitDeath();
    public OnUnitDeath onUnitDeath;

    public List<GameObject> enemyToCreateEachWave;
    public int amountOfRemainingEnemy;

    void Awake()
    {
        Instance = this;   
        enemyToCreateEachWave = new List<GameObject>();
    }

    public void AddEnemyToWave(GameObject enemy)
    {
        enemyToCreateEachWave.Add(enemy);
    }

    public void StartWave()
    {

    }
}

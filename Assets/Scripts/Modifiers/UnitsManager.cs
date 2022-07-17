using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public GameObject player;
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
        List<ModifiersData> modifiers = ModifierManager.Instance.GetAllWorldModifier().FindAll(x => x.upgradeManifestation == ModifiersData.UpgradeManifestation.OnCreated);
        float i = 0;
        foreach (ModifiersData mod in modifiers)
        {
            CreateEnemy(mod, i);
            i += 0.75f;
        }
    }

    void CreateEnemy(ModifiersData mod, float waitTime)
    {
        foreach (ActionModifier action in mod.actionModifierList)
        {
            if (action.action == ActionModifier.Action.Create)
            {
                amountOfRemainingEnemy++;
                waitTime += 0.25f;
                StartCoroutine(CreateEnemyCoroutine(mod, waitTime));
            }
        }
    }

    IEnumerator CreateEnemyCoroutine(ModifiersData mod, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Transform tf = GetRandomSpawner();
        GameObject go = Instantiate(mod.objectToCreatePrefab[0], tf.position, Quaternion.identity);
        go.transform.SetParent(enemyParent);
    }


    public void KillUnit(CharacterHealth character)
    {
        if (character.gameObject.CompareTag("Enemy"))
        {
            Destroy(character.gameObject);
            amountOfRemainingEnemy--;
        }
    }

    Transform GetRandomSpawner()
    {
        int rnd = Random.Range(0, spawnerPoints.Length);
        return spawnerPoints[rnd];
    }

    public void LockPlayer(bool boo)
    {
        player.GetComponent<CharacterMovement>().Lock(boo);
    }

    public void ResetPlayer()
    {
        player.GetComponent<ModifierHandler>().SetupModifiers();
    }
}

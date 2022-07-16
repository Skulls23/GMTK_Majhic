using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject countDown;
    public TextMeshProUGUI countDownText;
    private int currentRound = 1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UnitsManager.Instance.Init();
        ModifierManager.Instance.Init();

        StartCoroutine(StartARound());
    }


    IEnumerator StartARound()
    {
        Debug.Log("[Start a Round] :: Round " + currentRound);
        //on attends 2-3 secondes avant d'envoyer la sauce
        StartCoroutine(CallCountDown(3));
        yield return new WaitForSeconds(3f);

        Debug.Log("[Launch Dice]");

        // On lance un dé et on attends son résultat
        //lancer le dé

        //yield return new WaitUntil(() => bool), avec bool quand le jeu a tiré le modifier et montré au joueur (une sorte de isReady)
        Debug.Log("[End launch Dice] : Res = ?"); // dice results

        Debug.Log("[Prepare Battle phase]");

        // Rétablir la scéne et le controle du personnage au joueur

        //on attends 2-3 secondes avant d'envoyer la sauce
        StartCoroutine(CallCountDown(3));
        yield return new WaitForSeconds(3f);

        Debug.Log("[Start Battle phase]");

        //on attends que le joueur ait tué tous les ennemis
        yield return new WaitUntil(() => UnitsManager.Instance.amountOfRemainingEnemy <= 0);
        Debug.Log("[End Battle phase] : Player killed all enemies");

        //on attends 2-3 secondes avant d'envoyer la sauce
        StartCoroutine(CallCountDown(3));
        yield return new WaitForSeconds(3f);

        Debug.Log("[Choose Modifier Phase]");

        // on display au joueur les modificateurs à choisir

        //on attends qu il choisisse le modificateur et ait appuyé sur 'Pret'
        // yield return new WaitUntil(()=> buttonIsReadyIsPressed);

        Debug.Log("[Player Choosed Modifier] : Modifier Chosen = ?");
        Debug.Log("[Preparing Next round]");

        //on attends 2-3 secondes avant d'envoyer la sauce
        StartCoroutine(CallCountDown(3));
        yield return new WaitForSeconds(3f);

        currentRound++;
        Debug.Log("[----------------------------------]");
        StartCoroutine(StartARound());
    }

    IEnumerator CallCountDown(float duration)
    {
        countDown.SetActive(true);
        countDownText.text = duration.ToString();

        yield return new WaitForSeconds(1f);
        duration--;

        if(duration> 0)
        {
            StartCoroutine(CallCountDown(duration));
        }
        else
            countDown.SetActive(false);
    }
}

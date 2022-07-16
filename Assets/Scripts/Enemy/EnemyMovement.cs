using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private string     enemyCode;
    [SerializeField] private GameObject player;

    private Rigidbody2D rb;
    private UnitStats   unitStats;


    private void Awake()
    {
        unitStats = GetComponent<UnitStats>();
        rb        = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < Screen.width && screenPos.x > 0 && screenPos.y < Screen.height && screenPos.y > 0)
        {
            //We will define the type of movement dependant of the type of the enemy
            switch (enemyCode)
            {
                case "Agressive1": //go straight forward, bounce on walls | shoot towards the player
                    Agressive1();
                    break;
                case "Agressive2": //go toward the player
                    Agressive2();
                    break;
                case "Agressive3": //Do the same moves as the player      | shoot towards the player
                    break;
                case "Agressive4": //As far from the player as possible   | Shoot 3 bullet like a shotgun
                    break;
                case "Agressive5": //Randomly moves
                    break;
                case "Passive1"  : //Do nothing
                    break;
                case "Passive2"  : //Slowly move randomly
                    break;
                case "Passive3"  : //Try to be on Player's path
                    break;
                case "Passive4X" : //Move on X
                    break;
                case "Passive4Y" : //Move on Y
                    break;

            }
        }
        else if (enemyCode == "Agressive1")
        {
            print("Here will be the bouncing effet");
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (screenPos.x >= Screen.width)
                transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, 0);
            else if (screenPos.x <= 0)
                transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, 0);
            else if (screenPos.y >= Screen.height)
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
            else if (screenPos.y >= 0)
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, 0);
        }
    }

    private void Agressive1()
    {
        rb.velocity = transform.right * unitStats.MaximumMoveSpeed;
    }

    private void Agressive2()
    {
        RotateTowardTarget(player);
        rb.velocity = transform.right * unitStats.MaximumMoveSpeed;
    }

    private void RotateTowardTarget(GameObject target)
    {
        Vector3 direction = Camera.main.WorldToScreenPoint(target.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /////////////////////////
    /// GETTERS & SETTERS ///
    /////////////////////////

    public string EnemyCode
    {
        get { return enemyCode; }
        set { enemyCode = value; }
    }

    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

    public UnitStats UnitStats
    {
        get { return unitStats; }
        set { unitStats = value; }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyCodeEnum enemyCode; // string auparavant
    [SerializeField] private GameObject     player;

    private Rigidbody2D playerRB;
    private Vector2     playerDirection;
    private Rigidbody2D rb;
    private UnitStats   unitStats;
    private Vector3     positionOnScreen;
    private bool        isFirstDirection = true;

    private Camera cam;
    public enum EnemyCodeEnum
    {
        Agressive1,
        Agressive2,
        Agressive3,
        Agressive4,
        Agressive5,
        Passive2,
        Passive3,
        Passive4X,
        Passive4Y
    }

    private Vector2 direction;


    private void Awake()
    {
        unitStats = GetComponent<UnitStats>();
        rb        = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        cam = Camera.main;        
    }

    void Start()
    {
        player = UnitsManager.Instance.player;
        direction = Vector2.one;
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        positionOnScreen = cam.WorldToScreenPoint(transform.position);

        if ((positionOnScreen.x < Screen.width && positionOnScreen.x > 0 && positionOnScreen.y < Screen.height && positionOnScreen.y > 0) ||
            enemyCode != EnemyCodeEnum.Agressive1)
        {
            //We will define the type of movement dependant of the type of the enemy
            switch (enemyCode)
            {
                case EnemyCodeEnum.Agressive1: //go straight forward, bounce on walls | shoot towards the player
                    Agressive1();
                    break;
                case EnemyCodeEnum.Agressive2: //go toward the player
                    Agressive2();
                    break;
                case EnemyCodeEnum.Agressive3: //Do the same moves as the player      | shoot towards the player
                    Agressive3();
                    break;
                case EnemyCodeEnum.Agressive4: //As far from the player as possible   | Shoot 3 bullet like a shotgun
                    break;
                case EnemyCodeEnum.Agressive5: //Randomly moves
                    break;
                // case EnemyCodeEnum.Passive1  : //Do nothing
                //     break;
                case EnemyCodeEnum.Passive2  : //Slowly move randomly
                    break;
                case EnemyCodeEnum.Passive3  : //Try to be on Player's path
                    break;
                case EnemyCodeEnum.Passive4X : //Move on X
                    Passive4('x');
                    break;
                case EnemyCodeEnum.Passive4Y : //Move on Y
                    Passive4('y');
                    break;

            }
        }
        else if (enemyCode == EnemyCodeEnum.Agressive1)
        {
            print("Here will be the bouncing effet");
            direction.x *= -1;
            Agressive1();
        }
    }

    private void Agressive1()
    {
        rb.velocity = transform.right * unitStats.MaximumMoveSpeed * direction;
    }

    private void Agressive2()
    {
        RotateTowardTarget(player);
        rb.velocity = transform.right * unitStats.MaximumMoveSpeed;
    }

    private void Agressive3()
    {
        playerDirection = playerRB.velocity.normalized;
        rb.velocity = playerDirection * unitStats.MaximumMoveSpeed;
    }

    private void Passive4(char axis)
    {
        print(positionOnScreen);
        print(Screen.width);
        if (axis == 'x')
        {
            if(positionOnScreen.x >= Screen.width)
                isFirstDirection = false;
            else if(positionOnScreen.x <= 0)
                isFirstDirection = true;
            
            
            if(isFirstDirection)
                rb.velocity = Vector2.right * unitStats.MaximumMoveSpeed;
            else
                rb.velocity = Vector2.left * unitStats.MaximumMoveSpeed;
        }
        else
        {
            if (positionOnScreen.y >= Screen.height)
                isFirstDirection = false;
            else if (positionOnScreen.y <= 0)
                isFirstDirection = true;

            print(isFirstDirection);

            if (isFirstDirection)
                rb.velocity = Vector2.up * unitStats.MaximumMoveSpeed;
            else
                rb.velocity = Vector2.down * unitStats.MaximumMoveSpeed;
        }
    }

    private void RotateTowardTarget(GameObject target)
    {
        Vector3 direction = cam.WorldToScreenPoint(target.transform.position) - cam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /////////////////////////
    /// GETTERS & SETTERS ///
    /////////////////////////

    public string EnemyCode
    {
        get { return enemyCode.ToString(); }
        set { Enum.TryParse<EnemyCodeEnum>(value, out enemyCode); } // enemyCode = 
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    void Start()
    {
        health = GetComponent<UnitStats>().MaximumHitPoint;
    }
    private float health;

    public float Health
    {
        get { return GetComponent<UnitStats>().MaximumHitPoint; }
        set
        {
            if (value < 9)
                health = value;
            else if (value >= 9)
                health = 8;
            else if(value <= 0)
                Die();
        }
    }

    public void Hit(int amount)
    {
        health -= amount;

        if(health <= 0)
            Die();
    }

    public void Die()
    {
        Application.Quit();
    }
}

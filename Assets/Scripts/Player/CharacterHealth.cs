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
        get { return health; }
        set { health = value; }
    }

    public void Hit(int amount)
    {
        health -= amount;

        if(health <= 0)
            Die();
    }

    public void Die()
    {
        UnitsManager.Instance.KillUnit(this);
    }
}

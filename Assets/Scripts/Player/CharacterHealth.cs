using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{   
    [SerializeField]private Animator animator;
    private int deathTriggerHash;
    void Start()
    {
        deathTriggerHash = Animator.StringToHash("TriggerDeath");
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
        if(animator){ animator.SetTrigger(deathTriggerHash);}
        UnitsManager.Instance.KillUnit(this);
    }
}

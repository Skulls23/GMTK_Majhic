using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance;
    public delegate void OnUnitDeath();
    public OnUnitDeath onUnitDeath;

    void Awake()
    {
        Instance = this;   
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float health = 3;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }
}

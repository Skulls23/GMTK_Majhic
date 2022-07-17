using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnTrigger : MonoBehaviour
{
    public string targetTag = "Player";
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(targetTag))
        {
            if(other.TryGetComponent<CharacterHealth>(out CharacterHealth health))
            {
                health.Hit(1);
            }
        }
    }
}

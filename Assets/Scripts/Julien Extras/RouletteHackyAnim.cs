using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteHackyAnim : MonoBehaviour
{
    private SpriteRenderer rndr;
    private float delay = 0.2f;
    private float nextHit = 0;
    private float time;

#region Unity
    void Start()
    {
        rndr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        if(time > nextHit){
            nextHit += delay;
            rndr.flipX = !(rndr.flipX);
        }
    }
#endregion
}

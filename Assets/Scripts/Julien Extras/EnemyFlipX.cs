using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlipX : MonoBehaviour
{    
    private SpriteRenderer rndr;
    private Transform player;
    private Camera cam;

    private void Awake() {
        cam = Camera.main;
    }

    void Start()
    {
        player = UnitsManager.Instance.player.transform;
        rndr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rndr.flipX = cam.WorldToScreenPoint(player.transform.position).x - cam.WorldToScreenPoint(transform.position).x > 0;//this.transform.position.x - player.position.x < 0;       
    }
}

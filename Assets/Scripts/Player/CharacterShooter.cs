using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    [SerializeField] private float     fireRate = 0.5f;
    [SerializeField] private Transform firingPoint;

    private CharacterAiming characterAimingScript;
    private GameObject      bulletPrefab;
    private float           timeUntilNextShot;

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        characterAimingScript = GetComponent<CharacterAiming>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && timeUntilNextShot < Time.time)
        {
            Shoot();
            timeUntilNextShot = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        float angle = characterAimingScript.Angle;
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    //[SerializeField] private float fireRate = 0.5f;
    [SerializeField] private Transform firingPoint;

    private CharacterAiming characterAimingScript;
    private GameObject bulletPrefab;
    private float timeUntilNextShot;
    private UnitStats unitStats;

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        characterAimingScript = GetComponent<CharacterAiming>();
        unitStats = GetComponent<UnitStats>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && timeUntilNextShot < Time.time)
        {
            Shoot();
            timeUntilNextShot = Time.time + unitStats.CurrentNbAtkPerSecond;
        }
    }

    private void Shoot()
    {
        float angle = characterAimingScript.Angle;

        if(unitStats.onShoot != null)
            unitStats.onShoot();
            
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
